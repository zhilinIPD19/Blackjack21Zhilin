using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blackjack21Zhilin.Models;
using Blackjack21Zhilin.Utilies;
using Blackjack21Zhilin.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Blackjack21Zhilin.Controllers
{
    public class HomeController : Controller
    {
        private readonly Blackjack21ZhilinContext _context;
        public static Player player = new Player() { Amount = 500, Email = "zhilin.lin@mail.com", Password = "Zl123456" };
        public static CurrentViewModel model = new CurrentViewModel();
        public static List<PlayCard> playCards = new List<PlayCard>();
        public static List<PlayCard> playerHand = new List<PlayCard>();
        public static List<PlayCard> dealerHand = new List<PlayCard>();
        public static int playerValue = 0;
        public static int dealerValue = 0;
        public static int pBet = 0;
        public static int wins = 0;
        public static int loses = 0;
        public static int ties = 0;
        public static string message = "";
        public static string photoPathDealer = "";
        public HomeController(Blackjack21ZhilinContext context)
        {
            _context = context;
        }
       
        public IActionResult Bet()
        {                      
            model = new CurrentViewModel { Player = player};            
            return View(model);
        }

        [HttpPost]
        public IActionResult Bet(string bet)
        {
            bool isValidInt = Int32.TryParse(bet, out int playerBet);
            if (isValidInt && playerBet > 0 && playerBet <= player.Amount && playerBet <= 100)
            {
                player.Amount -= playerBet;
                pBet = playerBet;
                return RedirectToAction("Start", new CurrentViewModel { Bet = pBet, Message = "",Player=player }) ;
            }
            else
            {
                message = "Bet must be a number between 0 and 100, and less than your balance.";
                model.Message = message;
                return View(model);
            }
        }
        public IActionResult Start(CurrentViewModel model)
        {
            playerHand.Clear();
            dealerHand.Clear();
            playCards = GetAvailableCards();
            Utilitis.Shuffle(playCards);
            playerHand.Add(playCards[0]);
            playerHand.Add(playCards[1]);
            dealerHand.Add(playCards[2]);
            dealerHand.Add(playCards[3]);
            UpdateCards(playCards[0]);
            UpdateCards(playCards[1]);
            UpdateCards(playCards[2]);
            UpdateCards(playCards[3]);
            photoPathDealer = dealerHand[1].PhotoPath;
            dealerHand[1].PhotoPath = "/Images/Gray_back.jpg";
            playerValue = GetSumOfHand(playerHand);
            dealerValue = GetSumOfHand(dealerHand);

            model = new CurrentViewModel { PlayerHand = playerHand, DealerHand = dealerHand, Player = player, Loses = loses, Wins = wins, Ties = ties, PlayerValue = playerValue, DealerValue = 0, Bet = pBet, Message="", DisableHitStand = false, DisablePlayQuit = true,PlayCards = playCards };
            return View(model);
        }
        [HttpPost]
        public IActionResult Start(string Action)
        {
            bool disablePlayQuit = true;
            bool disableHitStand = false;
            playCards = GetAvailableCards();
            Utilitis.Shuffle(playCards);
            
            switch (Action)
            {
                case "Quit":
                    return RedirectToAction("Index");
                case "PlayAgain":
                    return RedirectToAction("Bet");
                case "Hit":
                    
                    playerHand.Add(playCards[0]);
                    playCards.Remove(playCards[0]);
                    playerValue = GetSumOfHand(playerHand);                    
                    if (playerValue > 21)
                    {
                        message = "You bust, you lose you bet.";
                        dealerHand[1].PhotoPath = photoPathDealer;
                        disablePlayQuit = false;
                        disableHitStand = true;
                        loses++;
                        CheckGameOver(player.Amount - pBet);
                    }
                    else if(playerValue == 21)
                    {
                        message = $"You Blackjack, You earn double bet.";
                        player.Amount += pBet * 3;
                        disablePlayQuit = false;
                        disableHitStand = true;
                        wins++;
                    }
                    else { message = "";
                    }
                    break;

                case "Stand":
                    dealerHand[1].PhotoPath = photoPathDealer;
                    dealerValue = GetSumOfHand(dealerHand);
                    if (dealerValue < 17)
                    {
                        dealerHand.Add(playCards[0]);
                        playCards.Remove(playCards[0]);
                        dealerValue = GetSumOfHand(dealerHand);
                    }
                    
                    playerValue = GetSumOfHand(playerHand);
                    if (dealerValue == 21)
                    {
                        message = "Dealer Blackjack, you lose you bet.";
                        disablePlayQuit = false;
                        disableHitStand = true;
                        loses++;
                        CheckGameOver(player.Amount - pBet);
                    }
                    else if (dealerValue > 21)
                    {
                        message = "Dealer bust, you earn bet.";
                        disablePlayQuit = false;
                        disableHitStand = true;
                        player.Amount += pBet * 2;
                        wins++;
                    }else if (dealerValue > playerValue)
                    {
                        message = "You lost, you lose you bet.";
                        disablePlayQuit = false;
                        disableHitStand = true;
                        loses++;
                        CheckGameOver(player.Amount-pBet);
                    } else if (dealerValue < playerValue)
                    {
                        message = "You win, you earn you bet.";
                        player.Amount += pBet * 2;
                        disablePlayQuit = false;
                        disableHitStand = true;
                        wins++;
                    }
                    else if (dealerValue == playerValue)
                    {
                        message = "You tie.";
                        player.Amount += pBet;
                        disablePlayQuit = false;
                        disableHitStand = true;
                        ties++;
                    }
                    break;                          
                default:
                    break;
            }
           model = new CurrentViewModel { PlayerHand = playerHand, DealerHand = dealerHand, Player = player, Loses = loses, Wins = wins, Ties = ties, PlayerValue = playerValue, DealerValue = 0, Bet = pBet, Message = message, DisableHitStand = disableHitStand, DisablePlayQuit = disablePlayQuit };
            
            return View(model);
        }    

        public IActionResult Index()
        {
           
            TempData["Amount"] = 1;
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<PlayCard> GetAvailableCards()
        {
            return ( from p in _context.PlayCard
                    where p.IsDistributed == false
                    select p).ToList() as List<PlayCard> ;
        }
        public void UpdateCards(PlayCard c)
        {
            var card = _context.PlayCard.First(a => a.PlayCardId == c.PlayCardId);
            card.IsDistributed = true;
            _context.SaveChanges();
        }
        public void CheckGameOver(int amount)
        {
            if (amount <= 0)
            {
                message = "Game Over!";
            }
           
        }
        public int GetSumOfHand(List<PlayCard> playCards)
        {
            int val = 0;
            int numAces = 0;

            foreach (PlayCard c in playCards)
            {
                if (c.Value == 1)
                {
                    numAces++;
                    val += 11;
                }
                else
                {
                    val += (int)c.Value;
                }
            }
            while (val > 21 && numAces > 0)
            {
                val -= 10;
                numAces--;
            }
            return val;
        }
    }
}
