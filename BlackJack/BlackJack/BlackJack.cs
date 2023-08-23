namespace BlackJack
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Runtime.InteropServices;

    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    // 카드 한 장을 표현하는 클래스
    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suit s, Rank r)
        {
            Suit = s;
            Rank = r;
        }

        public int GetValue()
        {
            if ((int)Rank <= 10)
            {
                return (int)Rank;
            }
            else if ((int)Rank <= 13)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }

        public override string ToString()
        {
            string suit;
            switch (Suit)
            {
                case Suit.Hearts:
                    suit = "♥";
                    break;
                case Suit.Diamonds:
                    suit = "◆";
                    break;
                case Suit.Clubs:
                    suit = "♣";
                    break;
                case Suit.Spades:
                    suit = "♠";
                    break;
                default:
                    suit = "";
                    break;
            }
            string rank;
            switch (Rank)
            {
                case Rank.Two: rank = "2";
                    break;
                case Rank.Three: rank = "3";
                    break;
                case Rank.Four: rank = "4";
                    break;
                case Rank.Five: rank = "5";
                    break;
                case Rank.Six: rank = "6";
                    break;
                case Rank.Seven: rank = "7";
                    break;
                case Rank.Eight: rank = "8";
                    break;
                case Rank.Nine: rank = "9";
                    break;
                case Rank.Ten: rank = "10";
                    break;
                case Rank.Jack: rank = "J";
                    break;
                case Rank.Queen: rank = "Q";
                    break;
                case Rank.King: rank = "K";
                    break;
                case Rank.Ace: rank = "A";
                    break;
                default: rank ="";
                    break;
            }

            return $"{suit}{rank}";
        }
    }

    // 덱을 표현하는 클래스
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(s, r));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = rand.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

    // 패를 표현하는 클래스
    public class Hand
    {
        private List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int GetTotalValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (Card card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                }
                total += card.GetValue();
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }
    }

    // 플레이어를 표현하는 클래스
    public class Player
    {
        public Hand Hand { get; private set; }

        public Player()
        {
            Hand = new Hand();
        }

        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return drawnCard;
        }
        public bool isBlackJack()
        {
            if (Hand.GetTotalValue() == 21)
                return true;
            else
                return false;
        }
        public bool isBust()
        {
            if (Hand.GetTotalValue() > 21)
                return true;
            else
                return false;
        }
        public bool lessThanScore17()
        {
            if (Hand.GetTotalValue() < 17)
                return true;
            else
                return false;
        }
    }

    // 블랙잭 게임
    public class Blackjack
    {
        Player player;
        Player dealer;
        Deck deck;
        bool gameEnded = false;
        bool roundEnded = false;
        public Blackjack()
        {
            player = new Player();
            dealer = new Player();
            deck = new Deck();
        }
        private void Init()
        {
            player = new Player();
            dealer = new Player();
            deck = new Deck();
        }
        public void StartGame()
        {
            while (!gameEnded)
            {
                Console.Clear();
                Console.WriteLine("블랙잭을 시작합니다.\n");
                Thread.Sleep(1500);
                DrawTwoCard();
                CheckBlackJackAndPush();

                if (roundEnded)
                {
                    AskRetry();
                    continue;
                }

                StartPlayerTurn();

                if (roundEnded)
                {
                    AskRetry();
                    continue;
                }

                StartDealerTurn();

                if (roundEnded)
                {
                    AskRetry();
                    continue;
                }

                CompareScore();

                if (roundEnded)
                {
                    AskRetry();
                    continue;
                }
            }
        }
        private void DrawTwoCard()
        {
            Console.WriteLine("딜러가 첫 카드를 뽑습니다.");
            Thread.Sleep(1500);
            Console.WriteLine("딜러의 첫 카드는 " + dealer.DrawCardFromDeck(deck).ToString() + " 입니다\n");
            Thread.Sleep(2000);
            Console.WriteLine("딜러가 플레이어 카드를 분배합니다.");
            Thread.Sleep(1500);
            Console.WriteLine("플레이어의 첫 카드는 " + player.DrawCardFromDeck(deck).ToString() + " 입니다");
            Thread.Sleep(1500);
            Console.WriteLine("플레이어의 두번째 카드는 " + player.DrawCardFromDeck(deck).ToString() + " 입니다\n");
            Thread.Sleep(2000);
            Console.WriteLine("딜러가 두번째 카드를 뽑습니다.\n");
            dealer.DrawCardFromDeck(deck);
            Thread.Sleep(1500);
        }
        private void CheckBlackJackAndPush()
        {
            Console.WriteLine("플레이어 또는 딜러가 블랙잭인지 확인합니다.\n");
            Thread.Sleep(1500);
            bool isPlayerBlackJack = player.isBlackJack();
            bool isDealerBlackJack = dealer.isBlackJack();
            if (isPlayerBlackJack)
            {
                Console.WriteLine("블랙잭입니다! 딜러의 블랙잭을 확인합니다.");
                Thread.Sleep(1500);
                if (isDealerBlackJack)
                {
                    Console.WriteLine("딜러도 블랙잭으로 푸시입니다. 무승부로 블랙잭이 종료되었습니다.\n");
                    Thread.Sleep(1500);
                    roundEnded = true;
                }
                else
                {
                    Console.WriteLine("딜러가 블랙잭이 아닙니다. 플레이어가 승리했습니다!\n");
                    Thread.Sleep(1500);
                    roundEnded = true;
                }
            }
            else if (isDealerBlackJack)
            {
                Console.WriteLine("딜러가 블랙잭입니다. 플레이어가 패배했습니다.\n");
                Thread.Sleep(1500);
                roundEnded = true;
            }
            else
            {
                Console.WriteLine("플레이어와 딜러 모두 블랙잭이 아닙니다.\n");
                Thread.Sleep(1500);
            }
        }
        private void AskRetry()
        {
            bool isCorrectAnswer = false;
            while (!isCorrectAnswer)
            {
                Console.Write("재시작 하시겠습니까? y / n : ");
                string? yesOrNo = Console.ReadLine();
                if (yesOrNo == "y")
                {
                    roundEnded = false;
                    isCorrectAnswer = true;
                    Init();
                    Console.WriteLine();
                }
                else if (yesOrNo == "n")
                {
                    gameEnded = true;
                    isCorrectAnswer = true;
                }
                else
                {
                    isCorrectAnswer = false;
                    Console.WriteLine("올바르지 못한 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
        }
        private void StartPlayerTurn()
        {
            bool turnEnded = false;
            while (!turnEnded)
            {
                Console.Write($"현재 플레이어의 점수 총합은 {player.Hand.GetTotalValue()}점 입니다. Hit 하시겠습니까? y / n : ");
                bool isCorrectAnswer = false;
                string? yesOrNo = "";
                while (!isCorrectAnswer)
                {
                    yesOrNo = Console.ReadLine();
                    if (yesOrNo == "y" || yesOrNo == "n")
                        isCorrectAnswer = true;
                    else
                        Console.WriteLine("\n올바르지 못한 입력입니다. Hit 하시겠습니까? y / n : ");
                }
                if (yesOrNo == "y")
                {
                    Console.WriteLine("\n딜러가 카드를 분배합니다.");
                    Thread.Sleep(1500);
                    Console.WriteLine("뽑힌 카드는 " + player.DrawCardFromDeck(deck).ToString() + " 입니다.\n");
                    Thread.Sleep(1500);
                    if (player.isBust())
                    {
                        Console.WriteLine($"현재 플레이어의 점수는 {player.Hand.GetTotalValue()}점으로 21점을 넘어 bust입니다. 패배했습니다.\n");
                        Thread.Sleep(1500);
                        turnEnded = true;
                        roundEnded = true;
                    };
                }
                else
                {
                    Console.WriteLine("\nStay합니다.\n");
                    turnEnded = true;
                    Thread.Sleep(1500);
                }
            }
        }
        private void StartDealerTurn()
        {
            Console.WriteLine("딜러의 차례입니다.\n");
            Thread.Sleep (1500);
            while (dealer.lessThanScore17())
            {
                Console.WriteLine($"현재 딜러의 점수는 {dealer.Hand.GetTotalValue()}점 입니다. 17점 미만이므로 Hit합니다.\n");
                Thread.Sleep(2000);
                Console.WriteLine("딜러가 뽑은 카드는 " + dealer.DrawCardFromDeck(deck).ToString() + " 입니다.");
                Thread.Sleep(1500);
                if (dealer.isBust())
                {
                    Console.WriteLine($"\n딜러가 {dealer.Hand.GetTotalValue()}점으로 21점을 넘어 bust입니다. 승리했습니다!\n");
                    Thread.Sleep(1500);
                    roundEnded = true;
                    return;
                }
                else
                    continue;
            }
            Console.WriteLine("\n딜러가 17점 이상이라 더이상 Hit하지 않습니다.\n");
            Thread.Sleep(1500);
        }
        private void CompareScore()
        {
            Console.WriteLine("딜러와 플레이어의 점수를 비교합니다. 21점에 더 가까운 쪽이 승리합니다.\n");
            Thread.Sleep(1500);
            int playerScoreGapWith21 = Math.Abs(player.Hand.GetTotalValue() - 21);
            int dealerScoreGapWith21 = Math.Abs(dealer.Hand.GetTotalValue() - 21);
            if (playerScoreGapWith21 < dealerScoreGapWith21)
            {
                Console.WriteLine($"플레이어의 점수 차이는 {playerScoreGapWith21}점 딜러의 점수 차이는 {dealerScoreGapWith21}점으로 플레이어의 승리입니다!\n");
                Thread.Sleep(1500);
                roundEnded = true;
            }
            else if (playerScoreGapWith21 == dealerScoreGapWith21)
            {
                Console.WriteLine($"플레이어의 점수 차이는 {playerScoreGapWith21}점 딜러의 점수 차이는 {dealerScoreGapWith21}점으로 동점입니다. 무승부입니다.\n");
                Thread.Sleep(1500);
                roundEnded = true;
            }
            else
            {
                Console.WriteLine($"플레이어의 점수 차이는 {playerScoreGapWith21}점 딜러의 점수 차이는 {dealerScoreGapWith21}점으로 딜러의 승리입니다. 패배했습니다.\n");
                Thread.Sleep(1500);
                roundEnded = true;
            }
        }
    }

    // 메인에서는 Blackjack 클래스 만들고 StartGame()만 실행
    class Program
    {
        static void Main(string[] args)
        {
            Blackjack blackjack = new Blackjack();
            blackjack.StartGame();
        }
    }
}