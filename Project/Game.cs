using System.Collections.Generic;
using System;
using System.Linq;

namespace CastleGrimtol.Project
{
    public class Game : IGame
    {
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Room> Rooms = new List<Room>();
        public bool playing = true;
        public void Setup()
        {   //Rooms:
            Room room1 = new Room("Pyramid Dungeon", @"You're in a dusty dungeon with cobwebs and a musky smell. All you can
            remember is you were hunting for the mythical treasure the pyramid contains, and you
            ended up here... |"); //1
            //Current room set to start at room1
            CurrentRoom = room1;
            Room room2 = new Room("Eastern Wing", @"After jumping over and over to push the grate off of your 
            holding cell, you've managed to crawl up out of of the dungeon. You remember 
            being brought here by a group of religious maniacs who are rumored to guard the treasure. 
            Let's hope they're not nearby..."); //2
            Room room3 = new Room("Central Pyramid", @"You step into a large open room. Torches light up cryptic
             heiroglyphs that you vaguely remember seeing. You think the treasure may be located to the right,
             but you can't remember for sure. You see a shadow in the shape of a large figure in the opposite 
             corner of the room. It may be dangerous..."); //3
            Room room4 = new Room("Ventilation Shaft", @"Cramped and drafty, you've found yourself in what appears to be a 
            ventilation shaft of sorts. Stuffy air rushes past your shoulders to what 
            is surely the exit. Adrenaline courses through your veins. You can taste your 
            freedom. The treasure doesn't matter anymore. Not as much as sunlight."); //4
            Room room5 = new Room("Western Wing", @"You've entered into a circular room. There are steps in the center,
            resembling a lecture room of sorts. It's hard to see what's down there.."); //5
            Room room6 = new Room("Western Corridor", @"You wander through the doorway and into a hallway. This seems like a
            booby trap waiting to happen. It's hard to see what's at the very end of the hallway.."); //6
            Room room7 = new Room("COLLAPSING CHAMBER", "THE ROOM IS COLLAPSING"); //7 trap
            Room room8 = new Room("SNAKE PIT", "A PIT FULL OF SNAKES."); //8 trap
            Room roomTop = new Room("Treasure Room", @"Can it be? A room lined with walls of plated gold and gems embedded
            into the walls. The flames refract soft golden light in all directions. Most likely a 
            tomb of an ancient great leader, now just a treasure chest waiting to be found. Now it has
            been! Best keep this secret to yourself! You feel a heavy draft from above.."); //9
            Room outside = new Room("Outside", @"Congradulations! You've escaped with the hidden treasure! Your trusted
            steed, Benny the Puma has been waiting for you patiently at the top of the pyramid. It's 
            like he could smell you through the vents. You better go home and shower..."); //10

            //Room relationships:
            room1.Directions["up"] = room2;
            room2.Directions["right"] = room3;
            room3.Directions["up"] = room4;
            room3.Directions["right"] = room5;
            room4.Directions["up"] = roomTop;
            room3.Directions["right"] = room5;
            room3.Directions["left"] = room2;
            room5.Directions["right"] = room6;
            room5.Directions["left"] = room3;
            room6.Directions["left"] = room5;
            room5.Directions["down"] = room8;
            room6.Directions["right"] = room7;

            //Types of room items:
            Item torch = new Item("torch", "set fire to the rain", true);
            Item ceramicJar = new Item("ceramic jar", "holds things. breakable.", false);
            Item goldCoins = new Item("gold coins", "much shiny. very value.", false);
            Item dagger = new Item("dagger", "you lost this earlier", true);
            Item treasure = new Item("treasure", "emeralds and rubies and gold oh my!", false);

            //Add items to rooms:
            room1.Items.Add(torch);
            room2.Items.Add(torch);
            room2.Items.Add(ceramicJar);
            room2.Items.Add(goldCoins);
            room3.Items.Add(torch);
            room3.Items.Add(ceramicJar);
            room3.Items.Add(goldCoins);
            roomTop.Items.Add(treasure);
            room5.Items.Add(dagger);

            //Print the room after it's setup
            PrintRoom(CurrentRoom, $@"Are you ready to face your fate, {CurrentPlayer.Name}? 
                     THE ONLY RULE: YOU AREN'T ALLOWED TO LEAVE WITHOUT THE TREASURE.");
        }
        public void Reset()
        {
            Setup();
        }
        public void Menu()
        {
            System.Console.WriteLine($@"                                      Have you planned your escape?
                        You can say: 'go <up, down, left, right>', 'use <item>', 
                                    'take <item>' or 'help' or 'quit'
            ----------------------------------------------------------------------------------------");

            string choice = Console.ReadLine().ToLower();
            if (choice.Contains("take"))
            {
                if (choice.Contains("torch"))
                {
                    Item torch = CurrentPlayer.Inventory.Find(t => t.Name == "torch");
                    if (CurrentPlayer.Inventory.Contains(torch))
                    {
                        PrintRoom(CurrentRoom, @"You already have a reusable torch.");
                    }
                    else
                    {
                        TakeItem("torch");
                    }
                }
                else if (choice.Contains("ceramic"))
                {
                    PrintRoom(CurrentRoom, $@"You can't take this item.");
                }
                else if (choice.Contains("chest"))
                {
                    UseItem("chest");
                }
                else if (choice.Contains("coins"))
                {
                    TakeItem("gold coins");
                }
                else if (choice.Contains("dagger"))
                {
                    TakeItem("dagger");
                }
                else if (choice.Contains("treasure"))
                {
                    TakeItem("treasure");
                }

            }
            else if (choice.Contains("use"))
            {
                if (choice.Contains("torch"))
                {
                    UseItem("torch");
                }
                else if (choice.Contains("ceramic"))
                {
                    foreach (Item item in CurrentRoom.Items.ToList())
                    {
                        if ("ceramic jar" == item.Name)
                        {
                            int index = CurrentRoom.Items.IndexOf(item);
                            CurrentRoom.Items.RemoveAt(index);
                        }
                    }
                    UseItem("ceramic jar");
                }
                else if (choice.Contains("coins"))
                {
                    UseItem("gold coins");
                }
                else if (choice.Contains("dagger"))
                {
                    UseItem("dagger");
                }
                else if (choice.Contains("chest"))
                {
                    UseItem("chest");
                }
                else if (choice.Contains("treasure"))
                {
                    UseItem("treasure");
                }


            }
            else if (choice.Contains("go"))
            {
                if (choice.Contains("up"))
                {
                    Navigate("up", CurrentPlayer.Inventory);
                }
                else if (choice.Contains("down"))
                {
                    Navigate("down", CurrentPlayer.Inventory);
                }
                else if (choice.Contains("left"))
                {
                    Navigate("left", CurrentPlayer.Inventory);
                }
                else if (choice.Contains("right"))
                {
                    Navigate("right", CurrentPlayer.Inventory);
                }
                else
                {
                    PrintRoom(CurrentRoom, "Please provide a valid input.");
                }

            }

            else if (choice.Contains("help"))
            {
                Help();
            }
            else if (choice.Contains("quit"))
            {
                Quit();
            }
            else
            {
                PrintRoom(CurrentRoom, "Please provide a valid input.");
            }

        }
        public void UseItem(string itemName)
        {
            if (itemName == "torch" && CurrentRoom.Name == "Pyramid Dungeon")
            {
                PrintRoom(CurrentRoom, $@"You've used {itemName}. You shine the light around the room and you observe a 
            ceiling vent. It appears to be the only opening in the room.");
            }
            else if (itemName == "ceramic jar" && CurrentRoom.Name == "Central Pyramid")
            {
                PrintRoom(CurrentRoom, $@"It appears you have upset the mummy and it is now chasing you! Run! Quick!");
            }
            else if (itemName == "torch" && CurrentRoom.Name == "Central Pyramid")
            {
                PrintRoom(CurrentRoom, $@"Looks like that thing in the corner of the room is a mummy.. just chillin..");
            }
            else if (itemName == "torch" && CurrentRoom.Name == "Eastern Wing")
            {
                PrintRoom(CurrentRoom, $@"The light from the torch illuminates doors to your left and right");
            }
            else if (itemName == "dagger" && CurrentRoom.Name == "Central Pyramid")
            {
                //kill the mummy?
            }
            else if (itemName == "torch" && CurrentRoom.Name == "Western Wing")
            {
                Item chest = new Item("chest", "possible treasure chest", false);
                CurrentRoom.Items.Add(chest);
                PrintRoom(CurrentRoom, $@"The light from the torch illuminates a chest in the center of the room. Could it 
                be what you've been looking for?");

            }
            else if (itemName == "chest" && CurrentRoom.Name == "Western Wing")
            {
                SnakePit();
            }
            else if (itemName == "treasure" && CurrentRoom.Name == "Top")
            {
                PrintRoom(CurrentRoom, $@"This is the treasure you were looking for! Hurry, and add it to your inventory!");
            }
            else if (itemName == "torch" || itemName == "dagger")
            {
                PrintRoom(CurrentRoom, $@"You've used {itemName}. Nothing happened.");
            }
            else if (itemName == "ceramic jar")
            {
                Item goldCoins = new Item("gold coins", "much shiny. very value.", false);
                CurrentRoom.Items.Add(goldCoins);
                // CurrentRoom.Items.Remove(ceramicJar);
                PrintRoom(CurrentRoom, $@"You've broken the {itemName}. There were coins inside!");

            }
            else if (itemName == "gold coins")
            {
                foreach (Item item in CurrentPlayer.Inventory.ToList())
                {
                    if ((itemName == item.Name) && (item.Reusable == false))
                    {
                        int index = CurrentPlayer.Inventory.IndexOf(item);
                        //remove from inventory
                        CurrentPlayer.Inventory.RemoveAt(index);
                        PrintRoom(CurrentRoom, $@"You've used {itemName} by throwing them away. What a waste.");
                        return;
                    }
                }
            }
            // FOR NON REUSABLE ITEMS:
            // foreach (Item item in CurrentPlayer.Inventory.ToList())
            //     {
            //         if ((itemName == item.Name) && (item.Reusable == false))
            //         {
            //             int index = CurrentPlayer.Inventory.IndexOf(item);
            //             //remove from inventory
            //             CurrentPlayer.Inventory.RemoveAt(index);
            //         }
            //     }
        }
        public void PrintRoom(Room currentRoom, string message)
        {
            if (currentRoom.Name != "Outside" && currentRoom.Name != "SNAKE PIT" && currentRoom.Name != "COLLAPSING CHAMBER")
            {
                Console.Clear();

                System.Console.WriteLine($@"
            =======================================================================================
                         _____                                                  _____
                        _|_[]|_                                                _|[]_|_
                      _/_/_|=\_\_                                            _/_/=|_\_\_
                    _/_ /_ |==\ _\_                                        _/_ /==| _\ _\_
                  _/__ /_ _|===\ __\_          PYRAMID PLUNDER           _/__ /===|_ _\ __\_
                _/_ _ /___ |====\  __\_                                _/_ _ /====| ___\  __\_
              _/ __ _/___ _|=====\ ___ \_                            _/ __ _/=====|_ ___\ ___ \_
            _/ ___ _/ ____ |======\_  __ \_                        _/ ___ _/======| ____ \_  __ \_
            =======================================================================================
            MESSAGE: {message}
            ----------------------------------------------------------------------------------------
            ROOM NAME: {currentRoom.Name} | 
            ROOM DESCRIPTION: {currentRoom.Description}
            SCORE: {CurrentPlayer.Score}
            ITEMS IN ROOM: ");
                foreach (Item item in CurrentRoom.Items)
                {
                    System.Console.WriteLine($@"
                {item.Name}");
                }
                System.Console.WriteLine($@"
            YOUR INVENTORY: ");
                foreach (Item item in CurrentPlayer.Inventory)
                {
                    System.Console.WriteLine($@"
                {item.Name}");
                }
                // How to cheat the system (if you have a map):
                // System.Console.WriteLine($@"
                // DIRECTIONS: ");
                // foreach (string dir in currentRoom.Directions.Keys)
                // {
                //     System.Console.WriteLine($@"
                //     {dir}");
                // }
                System.Console.WriteLine($@"
            ----------------------------------------------------------------------------------------");
            }
            else if (currentRoom.Name == "SNAKE PIT")
            {
                Console.Clear();

                System.Console.WriteLine($@"
            =======================================================================================
                         _____                                                  _____
                        _|_[]|_                                                _|[]_|_
                      _/_/_|=\_\_                                            _/_/=|_\_\_
                    _/_ /_ |==\ _\_                                        _/_ /==| _\ _\_
                  _/__ /_ _|===\ __\_          PYRAMID PLUNDER           _/__ /===|_ _\ __\_
                _/_ _ /___ |====\  __\_                                _/_ _ /====| ___\  __\_
              _/ __ _/___ _|=====\ ___ \_                            _/ __ _/=====|_ ___\ ___ \_
            _/ ___ _/ ____ |======\_  __ \_                        _/ ___ _/======| ____ \_  __ \_
            =======================================================================================
            MESSAGE: Oh no! A booby trap! You've fallen into the snake pit! What a 
                     pitty... The vipers relentlessly strike until you die a
                     painful death...
            ----------------------------------------------------------------------------------------
            ROOM NAME: {currentRoom.Name} | 
            ROOM DESCRIPTION: {currentRoom.Description}
            SCORE: {CurrentPlayer.Score}
            ITEMS IN ROOM: ");
                foreach (Item item in CurrentRoom.Items)
                {
                    System.Console.WriteLine($@"
                {item.Name}");
                }
                System.Console.WriteLine($@"
            YOUR INVENTORY: ");
                foreach (Item item in CurrentPlayer.Inventory)
                {
                    System.Console.WriteLine($@"
                {item.Name}");
                }
                System.Console.WriteLine($@"
            ----------------------------------------------------------------------------------------");
                LoseGame();
            }
            else if (currentRoom.Name == "COLLAPSING CHAMBER")
            {
                Console.Clear();

                System.Console.WriteLine($@"
            =======================================================================================
                         _____                                                  _____
                        _|_[]|_                                                _|[]_|_
                      _/_/_|=\_\_                                            _/_/=|_\_\_
                    _/_ /_ |==\ _\_                                        _/_ /==| _\ _\_
                  _/__ /_ _|===\ __\_          PYRAMID PLUNDER           _/__ /===|_ _\ __\_
                _/_ _ /___ |====\  __\_                                _/_ _ /====| ___\  __\_
              _/ __ _/___ _|=====\ ___ \_                            _/ __ _/=====|_ ___\ ___ \_
            _/ ___ _/ ____ |======\_  __ \_                        _/ ___ _/======| ____ \_  __ \_
            =======================================================================================
            MESSAGE: Oh no! A booby trap! The room has locked behind you
                    and is closing in on you. Your vision starts to fade as 
                    the stone quietly squeezes you as you breath your
                    last breath.
            ----------------------------------------------------------------------------------------
            ROOM NAME: {currentRoom.Name} | 
            ROOM DESCRIPTION: {currentRoom.Description}
            SCORE: {CurrentPlayer.Score}
            ITEMS IN ROOM: ");
                foreach (Item item in CurrentRoom.Items)
                {
                    System.Console.WriteLine($@"
                {item.Name}");
                }
                System.Console.WriteLine($@"
            YOUR INVENTORY: ");
                foreach (Item item in CurrentPlayer.Inventory)
                {
                    System.Console.WriteLine($@"
                {item.Name}");
                }
                System.Console.WriteLine($@"
            ----------------------------------------------------------------------------------------");
                LoseGame();
            }
            else
            {
                Console.Clear();

                System.Console.WriteLine($@"
            =======================================================================================
                         _____                                                  _____
                        _|_[]|_                                                _|[]_|_
                      _/_/_|=\_\_                                            _/_/=|_\_\_
                    _/_ /_ |==\ _\_                                        _/_ /==| _\ _\_
                  _/__ /_ _|===\ __\_          PYRAMID PLUNDER           _/__ /===|_ _\ __\_
                _/_ _ /___ |====\  __\_                                _/_ _ /====| ___\  __\_
              _/ __ _/___ _|=====\ ___ \_                            _/ __ _/=====|_ ___\ ___ \_
            _/ ___ _/ ____ |======\_  __ \_                        _/ ___ _/======| ____ \_  __ \_
            =======================================================================================
            MESSAGE: {message}
            ----------------------------------------------------------------------------------------
            ROOM NAME: {currentRoom.Name} | 
            ROOM DESCRIPTION: {currentRoom.Description}
            SCORE: {CurrentPlayer.Score}
            ITEMS IN ROOM: ");
                foreach (Item item in CurrentRoom.Items)
                {
                    System.Console.WriteLine($@"
                {item.Name}");
                }
                System.Console.WriteLine($@"
            YOUR INVENTORY: ");
                foreach (Item item in CurrentPlayer.Inventory)
                {
                    System.Console.WriteLine($@"
                {item.Name}");
                }
                System.Console.WriteLine($@"
            ----------------------------------------------------------------------------------------");
                WinGame();
            }
        }
        //No need to Pass a room since Items can only be used in the CurrentRoom
        public void TakeItem(string itemName)
        {
            Item torch = CurrentPlayer.Inventory.Find(t => t.Name == "torch");
            if (itemName != "treasure")
            {
                foreach (Item item in CurrentRoom.Items.ToList())
                {
                    if (itemName == item.Name)
                    {
                        int index = CurrentRoom.Items.IndexOf(item);
                        CurrentRoom.Items.RemoveAt(index);
                        //add to inventory
                        CurrentPlayer.Inventory.Add(item);
                    }
                }
                CurrentPlayer.Score += 10;
                PrintRoom(CurrentRoom, $"Item {itemName} successfully taken");
            }
            else if (itemName == "chest" && CurrentRoom.Name == "Western Wing")
            {
                SnakePit();
            }            
            else if (itemName == "treasure" && CurrentRoom.Name == "Treasure Room")
            {
                foreach (Item item in CurrentRoom.Items.ToList())
                {
                    if (itemName == item.Name)
                    {
                        int index = CurrentRoom.Items.IndexOf(item);
                        CurrentRoom.Items.RemoveAt(index);
                        //add to inventory
                        CurrentPlayer.Inventory.Add(item);
                    }
                }
                Room top = Rooms.Find(r => r.Name == "Top");
                Room outside = new Room("Outside", @"Congradulations! You've escaped with the hidden treasure! Your trusted
            steed, Benny the Puma has been waiting for you patiently at the top of the pyramid. It's 
            like he could smell you through the vents. You better go home and shower...");
                CurrentRoom.Directions["up"] = outside;
                CurrentPlayer.Score += 100;
                PrintRoom(CurrentRoom, $@"As you steal the treasure, you see a stone fall through the ceiling.
                Sunlight becomes visible, and the smell of fresh air fills your nostrils.");
            }
            else
            {
                foreach (Item item in CurrentRoom.Items.ToList())
                {
                    if (itemName == item.Name)
                    {
                        int index = CurrentRoom.Items.IndexOf(item);
                        CurrentRoom.Items.RemoveAt(index);
                        //add to inventory
                        CurrentPlayer.Inventory.Add(item);
                    }
                }
                //     Room top = Rooms.Find(r => r.Name == "Top");
                //     Room outside = new Room("Outside", @"Congradulations! You've escaped with the hidden treasure! Your trusted
                // steed, Benny the Puma has been waiting for you patiently at the top of the pyramid. It's 
                // like he could smell you through the vents. You better go home and shower...");
                //     CurrentRoom.Directions["up"] = outside;
                //     CurrentPlayer.Score += 100;
                PrintRoom(CurrentRoom, $"Item {itemName} successfully taken");
            }
        }
        public void SnakePit()
        {
            PrintRoom(CurrentRoom, @"
            Oh no! A booby trap! You've fallen into the snake pit! What a 
            pitty... The vipers relentlessly strike until you die a
            painful death...");
            LoseGame();
        }
        public void ClosedIn()
        {
            PrintRoom(CurrentRoom, @"
                Oh no! A booby trap! The room has locked behind you
                and is closing in on you. Your vision starts to fade as 
                the stone quietly squeezes you as you breath your
                last breath.");
        }
        public void Navigate(string dir, List<Item> inventory)
        {

            if (CurrentRoom.Directions.ContainsKey(dir))
            {
                foreach (string key in CurrentRoom.Directions.Keys)
                {
                    if (key == dir)
                    {
                        CurrentRoom = CurrentRoom.Directions[dir];
                        PrintRoom(CurrentRoom, $@"Successfully navigated to new room: {CurrentRoom.Name}");
                    }
                }
            }
            else if ((CurrentRoom.Name == "Top") && (dir == "up"))
            {
                {
                    Item treasure = CurrentPlayer.Inventory.Find(i => i.Name == "treasure");
                    if (!CurrentRoom.Directions.ContainsKey("up") && !CurrentPlayer.Inventory.Contains(treasure))
                    {
                        PrintRoom(CurrentRoom, $@"Wait! You can't leave without the treasure! Add it to your inventory!");
                    }
                    else
                    {
                        foreach (string key in CurrentRoom.Directions.Keys)
                        {
                            if (key == dir)
                            {
                                CurrentRoom = CurrentRoom.Directions[dir];
                                PrintRoom(CurrentRoom, $@"Successfully navigated to new room: {CurrentRoom.Name}");
                            }
                        }
                    }
                }
            }
            else if ((CurrentRoom.Name == "Western Corridor") && (dir == "right"))
            {
                ClosedIn();
            }
            else if ((CurrentRoom.Name == "Western Wing") && (dir == "down"))
            {
                SnakePit();
            }
            else
            {
                PrintRoom(CurrentRoom, @"After closer examination, you find that there is no exit over here. Please 
            provide a different direction. You can choose: up, down, left, or right.");
            }
        }
        public void LoseGame()
        {
            System.Console.WriteLine(@"
            ========================================================================================
                                                GAME OVER
            ========================================================================================");
            playing = false;
        }
        public void WinGame()
        {
            System.Console.WriteLine(@"
            ========================================================================================
                                                                                   * *    
                                 _____                                           *    *  *
                                _|[]_|_                                    *  *    *     *  *
                              _/_/=|_\_\_                               *     *    *  *    *
                            _/_ /==| _\ _\_                           * *   *    *    *    *   *
                          _/__ /===|_ _\ __\_                         *    *  *    * *    .#   *  *
                        _/_ _ /====| ___\  __\_                         *   *     * #.  .#   *   *
                      _/ __ _/=====|_ ___\ ___ \_                        *    '#.   #: #' * *  *
                    _/ ___ _/======| ____ \_  __ \_                     *   * * '#. ##'    *
                                                                           *       '###
                                                                                    '##
                                                                                      ##.
                                  (`.-,')                                             .##:
                                .-'     ;                                             :###
                            _.-'   , `,-                                              ;###
                      _ _.-'     .'  /._                                            ,####.
                    .' `  _.-.  /  ,'._;)                               /\/\/\/\/\/.######.\/\/\/\/\
                   (       .  )-| (
                    )`,_ ,'_,'  \_;)
            ('_  _,'.'  (___,))
             `-:;.-'                                                                                            
                                                YOU WON!
            ========================================================================================");
            playing = false;
        }
        public void Help()
        {
            System.Console.WriteLine(@"
            ----------------------------------------------------------------------------------------
                                                HELP MENU  
            ----------------------------------------------------------------------------------------");
            System.Console.WriteLine($@"
                Need some help?

                You can choose from the following options:
                You can say: 'go <up, down, left, right>', 'use <item>', 
                'take <item>' or 'help' or 'quit'.
                
                Format your input like this: go up, take torch, or use gold.
                To exit the game, type quit and hit enter.");
            System.Console.WriteLine(@"
            ----------------------------------------------------------------------------------------");
        }
        public void Quit()
        {
            System.Console.WriteLine($"Goodbye {CurrentPlayer.Name}! Thank you for playing!");
            playing = false;
        }
        public Player CreatePlayer()
        {
            System.Console.WriteLine("Welcome to Pyramid Plunder! What is your name, prisoner?");
            string pName = System.Console.ReadLine();
            System.Console.WriteLine($"Are you ready to meet your fate, {pName}?");
            Player p = new Player(pName);
            return p;
        }
        public Game()
        {
            CurrentPlayer = CreatePlayer();
            Setup();
        }
    }
}

