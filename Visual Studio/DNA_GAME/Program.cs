using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;


using System.Threading;


namespace DNA_game
{
    class Program
    {
        static char[,] screen = new char[8, 14];
        static bool stop_game = false;
        static bool start_falling = true, falling = true;
        static bool horizontal = true, vertical = false;
        static string falling_block, next_block;
        static bool left = false, right = false;
        static int rotate_number = 0;
        static bool bomb = false, bomb_begin = false;
        static int i, j;
        static int score = 0;
        static int milisecond;
        static string full_DNA_sequence = "", overlap_DNA_sequence = "";
        static bool down = false, up = false;
        static int snake_length = 2;
        static int k = 1, m = 28;
        static DateTime start = DateTime.Now;
        static Random r = new Random();

        static void Main(string[] args)
        {
            //SoundPlayer player = new SoundPlayer();
            //string path = "mario.wav"; // Çalmasını istediğiniz ses dosyasının yolu
            //player.SoundLocation = path;
            //player.Play();
            int level;
            int hold_milisecond;
            Console.Title = "DNA GAME";
            Console.CursorVisible = false;

            // Creating two-dimensional array
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 0 || i == 13)
                        screen[j, i] = '#';
                    else if (j == 0 || j == 7)
                        screen[j, i] = '#';
                    else
                        screen[j, i] = ' ';
                }
            }
            //------------------------------

            Console.ForegroundColor = ConsoleColor.Cyan;
            information();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(1, 17);
            Console.WriteLine("Please enter level : ");
            Console.WriteLine("1 --> Easy ");
            Console.WriteLine("2 --> Normal ");
            Console.WriteLine("3 --> Hard ");
            Console.SetCursorPosition(22, 17);
            do
            {
                level = Convert.ToInt16(Console.ReadLine());
                if (level != 1 && level != 2 && level != 3)
                {
                    Console.SetCursorPosition(17, 19);
                    Console.WriteLine("Please enter valid number");
                    Console.SetCursorPosition(22, 17);
                    Console.Write("           ");
                    Console.SetCursorPosition(22, 17);
                }
            } while (level != 1 && level != 2 && level != 3);

            if (level == 1)
                milisecond = 1000;
            else if (level == 2)
                milisecond = 500;
            else
                milisecond = 250;

            hold_milisecond = milisecond;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            string rgc1 = string.Concat(create_block(), create_block(), create_block());
            string rgc2 = string.Concat(create_block(), create_block(), create_block());
            string rgc3 = string.Concat(create_block(), create_block(), create_block());

            Console.SetCursorPosition(10, 4);
            Console.Write("Next Block : ");
            Console.SetCursorPosition(10, 5);
            Console.Write("Score : 0");
            Console.SetCursorPosition(10, 7);
            Console.Write("\tRGC");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(10, 9);
            Console.Write("--> " + rgc1);
            Console.SetCursorPosition(10, 10);
            Console.Write("--> " + rgc2);
            Console.SetCursorPosition(10, 11);
            Console.Write("--> " + rgc3);

            falling_block = create_block();

            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (screen[j, i] == '#')
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(screen[j, i]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Yellow;

            bool game_exit = false;

            while (!game_exit)
            {

                if (start_falling)
                {
                    start = DateTime.Now;
                    milisecond = hold_milisecond;
                    i = 3;
                    j = 1;
                    rotate_number = 0;
                    horizontal = true;
                    vertical = false;
                    if (r.Next(10) < 8)
                    {
                        next_block = create_block();
                    }
                    else
                    {
                        next_block = "X ";
                    }
                    Console.SetCursorPosition(23, 4);
                    Console.Write(next_block);

                    screen[3, 1] = falling_block[0];
                    screen[4, 1] = falling_block[1];
                    Console.SetCursorPosition(3, 1);
                    Console.Write(screen[3, 1]);
                    Console.SetCursorPosition(4, 1);
                    Console.Write(screen[4, 1]);

                    if (bomb_begin)
                    {
                        bomb = true;
                        bomb_begin = false;
                    }

                    if (screen[3, 2] == ' ' && screen[4, 2] == ' ')
                    {
                        start_falling = false;
                    }
                    else
                    {
                        start_falling = false;
                        falling = false;
                        game_exit = true;
                        Console.SetCursorPosition(23, 1);
                        Console.Write("  ");
                        Console.SetCursorPosition(25, 10);
                        Console.Clear();
                        game_end(overlap_DNA_sequence);
                        Console.ReadLine();
                    }
                } /* End of start_falling control */

                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);

                    switch (cki.Key)
                    {

                        case ConsoleKey.LeftArrow:
                            if (horizontal && !bomb && !stop_game)
                            {
                                if (screen[i - 1, j] == ' ')
                                {
                                    left = true;
                                    right = false;
                                    i--;
                                    vertical = false;
                                    screen[i, j] = screen[i + 1, j];
                                    screen[i + 1, j] = screen[i + 2, j];
                                    screen[i + 2, j] = ' ';
                                    Console.SetCursorPosition(i, j);
                                    Console.Write(screen[i, j]);
                                    Console.SetCursorPosition(i + 1, j);
                                    Console.Write(screen[i + 1, j]);
                                    Console.SetCursorPosition(i + 2, j);
                                    Console.Write(screen[i + 2, j]);
                                }
                            }
                            else if (vertical && !bomb && !stop_game)
                            {
                                if (screen[i - 1, j] == ' ' && screen[i - 1, j + 1] == ' ')
                                {
                                    left = true;
                                    right = false;
                                    i--;
                                    horizontal = false;
                                    screen[i, j] = screen[i + 1, j];
                                    screen[i, j + 1] = screen[i + 1, j + 1];
                                    screen[i + 1, j] = ' ';
                                    screen[i + 1, j + 1] = ' ';
                                    Console.SetCursorPosition(i, j);
                                    Console.Write(screen[i, j]);
                                    Console.SetCursorPosition(i, j + 1);
                                    Console.Write(screen[i, j + 1]);
                                    Console.SetCursorPosition(i + 1, j);
                                    Console.Write(screen[i + 1, j]);
                                    Console.SetCursorPosition(i + 1, j + 1);
                                    Console.Write(screen[i + 1, j + 1]);

                                }
                            }
                            else if (bomb && !stop_game)
                            {
                                if (screen[i - 1, j] == ' ')
                                {
                                    i--;
                                    screen[i, j] = screen[i + 1, j];
                                    screen[i + 1, j] = ' ';
                                    Console.SetCursorPosition(i, j);
                                    Console.Write(screen[i, j]);
                                    Console.SetCursorPosition(i + 1, j);
                                    Console.Write(screen[i + 1, j]);
                                }
                            }
                            break;

                        case ConsoleKey.RightArrow:
                            if (horizontal && !bomb && !stop_game)
                            {
                                if (screen[i + 2, j] == ' ')
                                {
                                    right = true;
                                    left = false;
                                    i++;
                                    vertical = false;
                                    screen[i + 1, j] = screen[i, j];
                                    screen[i, j] = screen[i - 1, j];
                                    screen[i - 1, j] = ' ';
                                    Console.SetCursorPosition(i + 1, j);
                                    Console.Write(screen[i + 1, j]);
                                    Console.SetCursorPosition(i, j);
                                    Console.Write(screen[i, j]);
                                    Console.SetCursorPosition(i - 1, j);
                                    Console.Write(screen[i - 1, j]);
                                }
                            }
                            else if (vertical && !bomb && !stop_game)
                            {
                                if (screen[i + 1, j] == ' ' && screen[i + 1, j + 1] == ' ')
                                {
                                    right = true;
                                    left = false;
                                    i++;
                                    horizontal = false;
                                    screen[i, j] = screen[i - 1, j];
                                    screen[i, j + 1] = screen[i - 1, j + 1];
                                    screen[i - 1, j] = ' ';
                                    screen[i - 1, j + 1] = ' ';
                                    Console.SetCursorPosition(i, j);
                                    Console.Write(screen[i, j]);
                                    Console.SetCursorPosition(i, j + 1);
                                    Console.Write(screen[i, j + 1]);
                                    Console.SetCursorPosition(i - 1, j);
                                    Console.Write(screen[i - 1, j]);
                                    Console.SetCursorPosition(i - 1, j + 1);
                                    Console.Write(screen[i - 1, j + 1]);
                                }
                            }

                            else if (bomb && !stop_game)
                            {
                                if (screen[i + 1, j] == ' ')
                                {
                                    i++;
                                    screen[i, j] = screen[i - 1, j];
                                    screen[i - 1, j] = ' ';
                                    Console.SetCursorPosition(i, j);
                                    Console.Write(screen[i, j]);
                                    Console.SetCursorPosition(i - 1, j);
                                    Console.Write(screen[i - 1, j]);
                                }
                            }

                            break;

                        case ConsoleKey.R:


                            // Rotate number = 0 means XY will be X    Rotate number = 1 means X will be YX
                            //                                    Y                            Y
                            // -------------------------------------------------------
                            // Rotate number = 2 means YX will be Y    Rotate number = 3 means Y will be XY                                                
                            //                                    X                            X
                            if (!bomb && !stop_game)
                            {
                                if (rotate_number % 4 == 0 && screen[i, j + 1] == ' ' && screen[i + 1, j + 1] == ' ')
                                {
                                    horizontal = false;
                                    vertical = true;
                                    screen[i, j + 1] = screen[i + 1, j];
                                    screen[i + 1, j] = ' ';
                                    Console.SetCursorPosition(i, j + 1);
                                    Console.Write(screen[i, j + 1]);
                                    Console.SetCursorPosition(i + 1, j);
                                    Console.Write(screen[i + 1, j]);
                                    rotate_number++;
                                }

                                else if (rotate_number % 4 == 1 && screen[i - 1, j] == ' ' && screen[i - 1, j + 1] == ' ')
                                {
                                    horizontal = true;
                                    vertical = false;
                                    screen[i - 1, j] = screen[i, j + 1];
                                    screen[i, j + 1] = ' ';
                                    Console.SetCursorPosition(i - 1, j);
                                    Console.Write(screen[i - 1, j]);
                                    Console.SetCursorPosition(i, j + 1);
                                    Console.Write(screen[i, j + 1]);
                                    i--;
                                    rotate_number++;
                                }

                                else if (rotate_number % 4 == 2 && screen[i + 1, j - 1] == ' ' && screen[i, j - 1] == ' ')
                                {
                                    horizontal = false;
                                    vertical = true;
                                    screen[i + 1, j - 1] = screen[i, j];
                                    screen[i, j] = ' ';
                                    Console.SetCursorPosition(i + 1, j - 1);
                                    Console.Write(screen[i + 1, j - 1]);
                                    Console.SetCursorPosition(i, j);
                                    Console.Write(screen[i, j]);
                                    i++;
                                    j--;
                                    rotate_number++;
                                }

                                else if (rotate_number % 4 == 3 && screen[i + 1, j + 1] == ' ' && screen[i + 1, j] == ' ')
                                {
                                    horizontal = true;
                                    vertical = false;
                                    screen[i + 1, j + 1] = screen[i, j];
                                    screen[i, j] = ' ';
                                    Console.SetCursorPosition(i + 1, j + 1);
                                    Console.Write(screen[i + 1, j + 1]);
                                    Console.SetCursorPosition(i, j);
                                    Console.Write(screen[i, j]);
                                    j++;
                                    rotate_number++;
                                }
                            }
                            break;

                        case ConsoleKey.DownArrow:
                            if (!stop_game)
                                milisecond = 10;

                            break;

                        case ConsoleKey.S:
                            if (!stop_game)
                            {
                                stop_game = true;
                            }
                            else
                            {
                                stop_game = false;
                            }
                            break;

                        #region SNAKE 
                        case ConsoleKey.B:
                            if (falling_block == "X ")
                                break;
                            bool snake = true;
                           
                                stop_game = true;

                                Console.SetCursorPosition(7, 1);
                                Console.Write(" ");
                                bool complete_screen = false;
                                int column = 8;
                                int row = 1;
                                Console.ForegroundColor = ConsoleColor.Green;
                                bool back = false;
                            
                                while (!complete_screen)
                                {
                                    TimeSpan diffbetween = DateTime.Now - start;
                                    int differ = Convert.ToInt16(diffbetween.TotalMilliseconds % Int16.MaxValue);
                                    if (column == 80 && row == 14)
                                        back = true;
                                    if (differ >= 10 && column < 80 && !back)
                                    {
                                        start = DateTime.Now;
                                        Console.SetCursorPosition(column, 0);
                                        Console.Write("#");
                                        column++;
                                    }
                                    else if (differ >= 10 && row < 14 && !back)
                                    {
                                        start = DateTime.Now;
                                        Console.SetCursorPosition(79, row);
                                        Console.Write("#");
                                        row++;
                                    }
                                    else if (differ >= 10 && column > 28 && back)
                                    {
                                        start = DateTime.Now;
                                        Console.SetCursorPosition(column - 2, 13);
                                        Console.Write("#");
                                        column--;
                                    }
                                    else if (differ >= 10 && row > 3 && back)
                                    {
                                        start = DateTime.Now;
                                        Console.SetCursorPosition(27, row - 2);
                                        Console.Write("#");
                                        row--;
                                    }
                                    else if (differ >= 10 && column > 8 && row == 3 && back)
                                    {
                                        start = DateTime.Now;
                                        Console.SetCursorPosition(column - 1, 2);
                                        Console.Write("#");
                                        column--;
                                    }
                                    else if (row == 3 && column == 8)
                                        complete_screen = true;
                                }
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                while (j != 1)
                                {
                                    TimeSpan diffbetween = DateTime.Now - start;
                                    int differ = Convert.ToInt16(diffbetween.TotalMilliseconds % Int16.MaxValue);
                                    if (differ >= 100)
                                    {
                                        start = DateTime.Now;
                                        Console.SetCursorPosition(i, j);
                                        Console.Write("  ");
                                        j--;
                                        Console.SetCursorPosition(i,j);
                                        Console.Write(falling_block);
                                    }
                                }
                                
                                while (i != 28)
                                {
                                    TimeSpan diffbetween = DateTime.Now - start;
                                    int differ = Convert.ToInt16(diffbetween.TotalMilliseconds % Int16.MaxValue);
                                    if (differ >= 100)
                                    {
                                        start = DateTime.Now;                                        
                                        i++;
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.SetCursorPosition(i + 1, j);
                                        Console.Write(falling_block[1]);
                                        Console.SetCursorPosition(i, j);
                                        Console.Write(falling_block[0]);
                                        Console.SetCursorPosition(i - 1, j);

                                        if (i > 7)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                            Console.Write("#");
                                        }
                                        else
                                        {                                           
                                            Console.Write(" ");
                                        }
                                    }
                                }

                                char[,] s_screen = new char[8, 80];
                                for (int a = 27; a < 80; a++)
                                {
                                    for (int b = 0; b < 8; b++)
                                    {
                                        if (a == 27 || a == 79)
                                            s_screen[b, a] = '#';
                                        else if (b == 0 || b == 7)
                                            s_screen[b, a] = '#';
                                        else
                                            s_screen[b, a] = ' ';
                                    }
                                }

                            

                                    s_screen[k, m] = falling_block[0];
                                    s_screen[k, m + 1] = falling_block[1];
                                    right = true;
                                    left=false;
                                    int hold_snakelength = 2;
                                    int hold_m = m;
                                    int down_number = 0;
                                while (snake)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    TimeSpan diffbetween = DateTime.Now - start;
                                    int differ = Convert.ToInt16(diffbetween.TotalMilliseconds % Int16.MaxValue);

                                    if (Console.KeyAvailable)
                                    { 
                                        ConsoleKeyInfo keyvalue = Console.ReadKey();

                                        if (keyvalue.Key == ConsoleKey.RightArrow)
                                        {
                                            right = true;
                                            left = false;
                                            up = false;
                                            down = false;
                                        }

                                        else if (keyvalue.Key == ConsoleKey.LeftArrow)
                                        {
                                            right = false;
                                            left = true;
                                            up = false;
                                            down = false;
                                        }

                                        else if (keyvalue.Key == ConsoleKey.UpArrow)
                                        {
                                            right = false;
                                            left = false;
                                            up = true;
                                            down = false;
                                        }

                                        else if (keyvalue.Key == ConsoleKey.DownArrow)
                                        {
                                            right = false;
                                            left = false;
                                            up = false;
                                            down = true;
                                        }
                                    
                                    }


                                    if (right && differ >= 50 && s_screen[k, m + 2] != '#' && snake_length > 0)
                                    {

                                        s_screen[k, m + 2] = s_screen[k, m + 1];
                                        Console.SetCursorPosition(m + 2, k);
                                        Console.Write(s_screen[k, m + 2]);
                                        m--;
                                        snake_length--;
                                        if (snake_length == 1)
                                        {
                                            s_screen[k, m] = ' ';
                                            Console.SetCursorPosition(m + 1, k);
                                            Console.Write(s_screen[k, m]);
                                        }

                                    }
                                    else
                                    {
                                        if (differ >= 50 && s_screen[k, m + 2] != '#')
                                        {
                                            start = DateTime.Now;
                                            m = hold_m++;
                                            m++;
                                            snake_length = hold_snakelength;
                                        }
                                    }
                                    snake_length = hold_snakelength;

                                    if (down && differ >= 50 && s_screen[k, m + 2] != '#' && snake_length > 0)
                                    {
                                        while (down_number != snake_length && s_screen[k+1,m+1]==' ')
                                        {
                                            s_screen[k + 1, m + 1] = s_screen[k, m + 1];
                                            s_screen[]
                                            Console.SetCursorPosition(m + 2, k);
                                            Console.Write(s_screen[k, m + 2]);

                                            //for (int x = snake_length; x > 0; x--)
                                            //{
                                            //    if (down_number > 0 && s_screen[k + 1, m + 1] == ' ')
                                            //    {
                                            //        s_screen[k + 1, m + 1] = s_screen[k, m + 1];
                                            //    }
                                            //}
                                        }
                                            s_screen[k, m + 2] = s_screen[k, m + 1];
                                        Console.SetCursorPosition(m + 2, k);
                                        Console.Write(s_screen[k, m + 2]);
                                        m--;
                                        snake_length--;
                                        if (snake_length == 1)
                                        {
                                            s_screen[k, m] = ' ';
                                            Console.SetCursorPosition(m + 1, k);
                                            Console.Write(s_screen[k, m]);
                                        }

                                    }
                                    else
                                    {
                                        if (differ >= 50 && s_screen[k, m + 2] != '#')
                                        {
                                            start = DateTime.Now;
                                            m = hold_m++;
                                            m++;
                                            snake_length = hold_snakelength;
                                        }
                                    }

                            }
                                

                            break;

                        #endregion
                    } /* End of switch */

                } /* End of key available control */


                if (!stop_game)
                {


                    TimeSpan diff = DateTime.Now - start;
                    int difference = Convert.ToInt16(diff.TotalMilliseconds % Int16.MaxValue);

                    if (difference >= milisecond && falling && !bomb)
                    {
                        start = DateTime.Now;

                        if (horizontal && screen[i, j + 1] == ' ' && screen[i + 1, j + 1] == ' ')
                        {
                            if (!left && !right)
                            {
                                fall();
                                j++;
                            }
                            else if (left || right)
                            {
                                fall();
                                j++;
                                left = false;
                                right = false;
                            }
                        }
                        else if (vertical && screen[i, j + 2] == ' ')
                        {
                            if (!left && !right)
                            {
                                fall();
                                j++;
                            }
                            if (left || right)
                            {
                                fall();
                                j++;
                                left = false;
                                right = false;
                            }
                        }

                        // End falling of falling_block
                        else if (vertical && screen[i, j + 1] != '#' && screen[1, j] != ' ' && screen[2, j] != ' ' && screen[3, j] != ' ' && screen[4, j] != ' ' && screen[5, j] != ' ' && screen[6, j] != ' ' && screen[1, j + 1] != ' ' && screen[2, j + 1] != ' ' && screen[3, j + 1] != ' ' && screen[4, j + 1] != ' ' && screen[5, j + 1] != ' ' && screen[6, j + 1] != ' ')
                        {
                            string top_string = string.Concat(screen[1, j], screen[2, j], screen[3, j], screen[4, j], screen[5, j], screen[6, j]);
                            string bottom_string = string.Concat(screen[1, j + 1], screen[2, j + 1], screen[3, j + 1], screen[4, j + 1], screen[5, j + 1], screen[6, j + 1]);

                            // Start to control first line
                            control(top_string, rgc1, rgc2, rgc3);
                            start = DateTime.Now;
                            // End of second line control

                            // Start to control first line 
                            control(bottom_string, rgc1, rgc2, rgc3);
                            //End of first line control

                            if (next_block == "X ")
                            {
                                bomb_begin = true;
                            }
                            falling_block = next_block;
                            start_falling = true;

                        }
                        else if (vertical && screen[i, j + 1] != '#' && screen[1, j + 1] != ' ' && screen[2, j + 1] != ' ' && screen[3, j + 1] != ' ' && screen[4, j + 1] != ' ' && screen[5, j + 1] != ' ' && screen[6, j + 1] != ' ')
                        {
                            string first_string = string.Concat(screen[1, j + 1], screen[2, j + 1], screen[3, j + 1], screen[4, j + 1], screen[5, j + 1], screen[6, j + 1]);
                            control(first_string, rgc1, rgc2, rgc3);

                            if (next_block == "X ")
                            {
                                bomb_begin = true;
                            }
                            falling_block = next_block;
                            start_falling = true;
                        }
                        else if (screen[1, j] != ' ' && screen[2, j] != ' ' && screen[3, j] != ' ' && screen[4, j] != ' ' && screen[5, j] != ' ' && screen[6, j] != ' ')
                        {
                            string second_string = string.Concat(screen[1, j], screen[2, j], screen[3, j], screen[4, j], screen[5, j], screen[6, j]);
                            control(second_string, rgc1, rgc2, rgc3);

                            if (next_block == "X ")
                            {
                                bomb_begin = true;
                            }
                            falling_block = next_block;
                            start_falling = true;
                        }
                        else
                        {
                            if (next_block == "X ")
                            {
                                bomb_begin = true;
                            }
                            falling_block = next_block;
                            start_falling = true;
                        }
                    }
                    if (difference >= milisecond && bomb)
                    {
                        start = DateTime.Now;

                        if (screen[i, j + 1] != ' ' && screen[i, j + 1] != '#')
                        {
                            screen[i, j + 1] = ' ';
                            screen[i, j] = ' ';
                            Console.SetCursorPosition(i, j + 1);
                            Console.Write(screen[i, j + 1]);
                            Console.SetCursorPosition(i, j);
                            Console.Write(screen[i, j]);
                            falling_block = next_block;
                            start_falling = true;
                            if (falling_block[0] != 'X')
                            {
                                bomb = false;
                            }
                        }
                        else if (screen[i, j + 1] == ' ')
                        {
                            screen[i, j + 1] = screen[i, j];
                            screen[i, j] = ' ';
                            Console.SetCursorPosition(i, j + 1);
                            Console.Write(screen[i, j + 1]);
                            Console.SetCursorPosition(i, j);
                            Console.Write(screen[i, j]);
                            j++;
                        }
                        else if (screen[i, j + 1] == '#')
                        {
                            screen[i, j] = ' ';
                            Console.SetCursorPosition(i, j);
                            Console.Write(screen[i, j]);
                            falling_block = next_block;
                            start_falling = true;
                            if (falling_block[0] != 'X')
                            {
                                bomb = false;
                            }
                        }

                    }
                }
            } /* End of while */

            Console.SetCursorPosition(0, 20);
            Console.Write(overlap_DNA_sequence);
            Console.ReadLine();

        } /* End of Main */


        static string create_block()
        {
            int first, last;
            first = r.Next(4);  // | 0 means A | 1 means G |
            last = r.Next(4);   // | 2 means T | 3 means C |
            if (first == 0 && last == 0)
                return "AA";
            else if (first == 0 && last == 1)
                return "AG";
            else if (first == 0 && last == 2)
                return "AT";
            else if (first == 0 && last == 3)
                return "AC";
            else if (first == 1 && last == 0)
                return "GA";
            else if (first == 1 && last == 1)
                return "GG";
            else if (first == 1 && last == 2)
                return "GT";
            else if (first == 1 && last == 3)
                return "GC";
            else if (first == 2 && last == 0)
                return "TA";
            else if (first == 2 && last == 1)
                return "TG";
            else if (first == 2 && last == 2)
                return "TT";
            else if (first == 2 && last == 3)
                return "TC";
            else if (first == 3 && last == 0)
                return "CA";
            else if (first == 3 && last == 1)
                return "CG";
            else if (first == 3 && last == 2)
                return "CT";
            else
                return "CC";
        } /* End of create_block function */

        static void fall()
        {
            if (horizontal)
            {
                screen[i, j + 1] = screen[i, j];
                screen[i + 1, j + 1] = screen[i + 1, j];
                screen[i, j] = ' ';
                screen[i + 1, j] = ' ';
                Console.SetCursorPosition(i, j + 1);
                Console.Write(screen[i, j + 1]);
                Console.SetCursorPosition(i + 1, j + 1);
                Console.Write(screen[i + 1, j + 1]);
                Console.SetCursorPosition(i, j);
                Console.Write(screen[i, j]);
                Console.SetCursorPosition(i + 1, j);
                Console.Write(screen[i + 1, j]);

            }
            else if (vertical)
            {
                screen[i, j + 2] = screen[i, j + 1];
                screen[i, j + 1] = screen[i, j];
                screen[i, j] = ' ';
                Console.SetCursorPosition(i, j + 2);
                Console.Write(screen[i, j + 2]);
                Console.SetCursorPosition(i, j + 1);
                Console.Write(screen[i, j + 1]);
                Console.SetCursorPosition(i, j);
                Console.Write(screen[i, j]);
            }

        } /* End of fall function */

        static string pair(string sequence)
        {

            for (int i = 0; i < sequence.Length; i++)
            {
                if (sequence[i] == 'A')
                {
                    sequence = sequence.Remove(i, 1);
                    sequence = sequence.Insert(i, "T");
                }
                else if (sequence[i] == 'T')
                {
                    sequence = sequence.Remove(i, 1);
                    sequence = sequence.Insert(i, "A");
                }
                else if (sequence[i] == 'G')
                {
                    sequence = sequence.Remove(i, 1);
                    sequence = sequence.Insert(i, "C");
                }
                else if (sequence[i] == 'C')
                {
                    sequence = sequence.Remove(i, 1);
                    sequence = sequence.Insert(i, "G");
                }
            }
            return sequence;
        } /* End of pair function */

        static string reverse(string sequence)
        {
            for (int i = 0, j = sequence.Length - 1; i < (sequence.Length) / 2; i++, j--)
            {
                char hold = sequence[i];
                sequence = sequence.Remove(i, 1);
                sequence = sequence.Insert(i, Convert.ToString(sequence[j - 1]));
                sequence = sequence.Remove(j, 1);
                sequence = sequence.Insert(j, Convert.ToString(hold));
            }
            return sequence;
        } /* End of reverse function */

        static string one_of_RGC(string sequence, string rgc_1, string rgc_2, string rgc_3)
        {
            if (sequence == rgc_1)
                return rgc_1;
            else if (sequence == rgc_2)
                return rgc_2;
            else if (sequence == rgc_3)
                return rgc_3;
            else
                return null;
        } /* End of one_of_RGC function */

        static string reverse_of_RGC(string sequence, string rgc_1, string rgc_2, string rgc_3)
        {
            sequence = reverse(sequence);
            if (sequence == rgc_1)
                return rgc_1;
            else if (sequence == rgc_2)
                return rgc_2;
            else if (sequence == rgc_3)
                return rgc_3;
            else
                return null;
        } /* End of reverse function */

        static string anagram_of_RGC(string sequence, string rgc_1, string rgc_2, string rgc_3)
        {
            string hold_sequence = sequence;
            string hold_rgc1 = rgc_1;
            string hold_rgc2 = rgc_2;
            string hold_rgc3 = rgc_3;
            bool anagram_rgc1 = true, anagram_rgc2 = true, anagram_rgc3 = true, loop = true;
            int index;
            char control;

            while (sequence.Length > 0 && loop)
            {
                control = sequence[sequence.Length - 1];
                index = rgc_1.IndexOf(control);
                if (index == -1)
                {
                    anagram_rgc1 = false;
                    loop = false;
                }
                else
                {
                    sequence = sequence.Remove(sequence.Length - 1, 1);
                    rgc_1 = rgc_1.Remove(index, 1);
                }
            }

            if (anagram_rgc1)
                return hold_rgc1;

            sequence = hold_sequence;
            loop = true;

            while (sequence.Length > 0 && loop)
            {
                control = sequence[sequence.Length - 1];
                index = rgc_2.IndexOf(control);
                if (index == -1)
                {
                    anagram_rgc2 = false;
                    loop = false;
                }
                else
                {
                    sequence = sequence.Remove(sequence.Length - 1, 1);
                    rgc_2 = rgc_2.Remove(index, 1);
                }
            }

            if (anagram_rgc2)
                return hold_rgc2;

            sequence = hold_sequence;
            loop = true;

            while (sequence.Length > 0 && loop)
            {
                control = sequence[sequence.Length - 1];
                index = rgc_3.IndexOf(control);
                if (index == -1)
                {
                    anagram_rgc3 = false;
                    loop = false;
                }
                else
                {
                    sequence = sequence.Remove(sequence.Length - 1, 1);
                    rgc_3 = rgc_3.Remove(index, 1);
                }
            }

            if (anagram_rgc3)
                return hold_rgc3;
            else
                return null;
        } /* End of anagram function */

        static void control(string constructed_seq, string rgc_1, string rgc_2, string rgc_3)
        {
            string user_string = "";
            int index = 0;
            bool exit = false, timeout = false, wait = true;

            Console.SetCursorPosition(0, 20);
            Console.Write(constructed_seq);
            Console.SetCursorPosition(10, 20);
            Console.Write("Time : ");

            int time = 0;

            while (!exit)
            {

                TimeSpan interval = DateTime.Now - start;
                int inner_difference = Convert.ToInt16(interval.TotalSeconds);
                Console.SetCursorPosition(18, 20);
                int old_time = time;

                time = 10 - inner_difference;

                if (time != old_time)
                {
                    Console.Write(time + "  ");
                }
                if (time == 0)
                {
                    exit = true;
                    timeout = true;
                }
                else if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key_value = Console.ReadKey(true);
                    string key_str = Convert.ToString(key_value.Key);

                    if (key_value.Key == ConsoleKey.A || key_value.Key == ConsoleKey.T || key_value.Key == ConsoleKey.G || key_value.Key == ConsoleKey.C)
                    {

                        user_string = user_string.Insert(index, key_str);
                        Console.SetCursorPosition(0, 22);
                        Console.Write(user_string);
                        index++;
                    }
                    else if (key_value.Key == ConsoleKey.Backspace)
                    {
                        index--;
                        user_string = user_string.Remove(index, 1);
                        Console.SetCursorPosition(0, 22);
                        Console.Write("           ");
                        Console.SetCursorPosition(0, 22);
                        Console.Write(user_string);

                    }

                }
                if (index == constructed_seq.Length)
                    exit = true;
            }


            if (timeout)
            {
                Console.SetCursorPosition(1, 16);
                Console.WriteLine("You are too late");
            }
            else
            {
                if (constructed_seq == pair(user_string))
                {
                    if (one_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) != null)
                    {
                        if (one_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_1)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed one of " + rgc_1 + ".. It means + 30 points!");
                            score += 30;
                            full_DNA_sequence += rgc_1;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_1);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }
                        else if (one_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_2)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed one of " + rgc_2 + ".. It means + 30 points!");
                            score += 30;
                            full_DNA_sequence += rgc_2;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_2);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }
                        else if (one_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_3)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed one of " + rgc_3 + ".. It means + 30 points!");
                            score += 30;
                            full_DNA_sequence += rgc_3;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_3);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }

                    }

                    else if (reverse_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) != null)
                    {
                        if (reverse_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_1)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed reverse of " + rgc_1 + ".. It means + 20 points!");
                            score += 20;
                            full_DNA_sequence += rgc_1;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_1);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }
                        else if (reverse_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_2)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed reverse of " + rgc_2 + ".. It means + 20 points!");
                            score += 20;
                            full_DNA_sequence += rgc_2;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_2);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }
                        else if (reverse_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_3)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed reverse of " + rgc_3 + ".. It means + 20 points!");
                            score += 20;
                            full_DNA_sequence += rgc_3;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_3);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }

                    }

                    else if (anagram_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) != null)
                    {
                        if (anagram_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_1)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed anagram of " + rgc_1 + ".. It means + 15 points!");
                            score += 15;
                            full_DNA_sequence += rgc_1;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_1);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }
                        else if (anagram_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_2)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed anagram of " + rgc_2 + ".. It means + 15 points!");
                            score += 15;
                            full_DNA_sequence += rgc_2;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_2);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }
                        else if (anagram_of_RGC(constructed_seq, rgc_1, rgc_2, rgc_3) == rgc_3)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.WriteLine("Congratulations!.. You are constructed anagram of " + rgc_3 + ".. It means + 15 points!");
                            score += 15;
                            full_DNA_sequence += rgc_3;
                            overlap_DNA_sequence = overlap(overlap_DNA_sequence, rgc_3);
                            Console.SetCursorPosition(18, 5);
                            Console.Write(score);
                        }

                    }

                    else
                    {
                        Console.SetCursorPosition(1, 16);
                        Console.WriteLine("Unfortunately you could not construct any valid sequence");
                    }

                    if (horizontal)
                        destroy_and_slide(j);
                    else if (vertical)
                        destroy_and_slide(j + 1);

                }

                else
                {
                    Console.SetCursorPosition(1, 16);
                    Console.WriteLine("Unfortunately you could not enter the pair of the sequence correctly");
                }

            }

            DateTime begin = DateTime.Now;

            while (wait)
            {
                TimeSpan wait_dif = DateTime.Now - begin;
                int waiting = Convert.ToInt16(wait_dif.TotalSeconds);
                if (waiting == 5)
                {
                    Console.SetCursorPosition(18, 20);
                    Console.Write("        ");
                    Console.SetCursorPosition(0, 16);
                    Console.WriteLine("                                                                                                  ");
                    Console.SetCursorPosition(0, 20);
                    Console.Write("                    ");
                    Console.SetCursorPosition(10, 20);
                    Console.Write("                    ");
                    Console.SetCursorPosition(0, 22);
                    Console.Write("                    ");
                    wait = false;
                }
            }

        } /* End of control function */

        static void destroy_and_slide(int row)
        {
            for (int i = 1; i < 7; i++)
            {
                screen[i, row] = ' ';
            }
            for (int i = 1; i < 7; i++)
            {
                for (j = row; j > 1; j--)
                {
                    screen[i, j] = screen[i, j - 1];
                    screen[i, j - 1] = ' ';
                    Console.SetCursorPosition(i, j);
                    Console.Write(screen[i, j]);
                    Console.SetCursorPosition(i, j - 1);
                    Console.Write(screen[i, j - 1]);
                }

            }

        } /* End of destroy and slide function */

        static public string overlap(string first, string second)
        {
            int counter = 0, lenght;
            string new_string;

            if (first.Length < second.Length)
            {
                lenght = first.Length;
                while (true)
                {
                    if (first.Substring(counter).Equals(second.Substring(0, lenght)))
                    {
                        break;
                    }
                    counter++;
                    lenght--;
                    if (lenght == 0)
                        break;
                }
                new_string = first + second.Substring(lenght);
            }
            else
            {
                lenght = second.Length;
                int difference = first.Length - second.Length;
                while (true)
                {
                    if (first.Substring(difference, lenght).Equals(second.Substring(0, lenght)))
                    {
                        counter = lenght;
                        break;
                    }
                    difference++;
                    lenght--;
                    if (lenght == 0)
                        break;
                }
                new_string = first + second.Substring(lenght);

            }

            return new_string;
        } /* End of overlap function */

        static void information()
        {
            Console.WriteLine("AAA  Lys\tAAC  Asn\tAAT  Asn\tAAG  Lys");
            Console.WriteLine("ATA  Ile\tATC  Ile\tAAT  Iln\tATG  Met  ( START )");
            Console.WriteLine("CAA  Gln\tCAC  His\tCAT  His\tCAG  Gln");                     // cac eksik
            Console.WriteLine("CTA  Leu\tCTC  Leu\tCTT  Leu\tCTG  Leu");
            Console.WriteLine("TAA  ( STOP )   TAC  Tyr\tTAT  Tyr\tTAG  ( STOP )");
            Console.WriteLine("TTA  Leu\tTTC  Phe\tTTT  Phe\tTTG  Leu");
            Console.WriteLine("GAA  Glu\tGAC  Asp\tGAT  Asp\tGAG  Glu");
            Console.WriteLine("GTA  Val\tGTC  Val\tGTT  Val\tGTG  Val");
            Console.WriteLine("ACA  Thr\tACC  Thr\tACT  Thr\tACG  Thr");
            Console.WriteLine("AGA  Arg\tAGC  Ser\tAGT  Ser\tAGG  Arg");
            Console.WriteLine("CCA  Pro\tCCC  Pro\tCCT  Pro\tCCG  Pro");
            Console.WriteLine("CGA  Arg\tCGC  Arg\tCGT  Arg\tCGG  Arg");
            Console.WriteLine("TCA  Ser\tTCC  Ser\tTCT  Ser\tTCG  Ser");
            Console.WriteLine("TGA  ( STOP )   TGC  Cys\tTGT  Cys\tTGG  Trp");
            Console.WriteLine("GCA  Ala\tGCC  Ala\tGCT  Ala\tGCG  Ala");
            Console.WriteLine("GGA  Gly\tGGC  Gly\tGGT  Gly\tGGG  Gly");
        } /* End of information function */

        static void game_end(string DNA)
        {
            string amino_asit, protein = "";
            int length = DNA.Length;

            if (DNA.Length % 3 != 0)
            {
                if (DNA.Length % 3 == 1)
                    length -= 1;
                else if (DNA.Length % 3 == 2)
                    length -= 2;
            }

            for (int i = 0; i < length; i += 3)
            {
                if (DNA.Substring(i, 3) == "AAA" || DNA.Substring(i, 3) == "AAG")
                {
                    amino_asit = "Lys";
                }
                else if (DNA.Substring(i, 3) == "AAC" || DNA.Substring(i, 3) == "AAT")
                {
                    amino_asit = "Asn";
                }
                else if (DNA.Substring(i, 3) == "ATA" || DNA.Substring(i, 3) == "ATC" || DNA.Substring(i, 3) == "ATT")
                {
                    amino_asit = "Ile";
                }
                else if (DNA.Substring(i, 3) == "CAA" || DNA.Substring(i, 3) == "CAG")
                {
                    amino_asit = "Gln";
                }
                else if (DNA.Substring(i, 3) == "GAC" || DNA.Substring(i, 3) == "CAT")
                {
                    amino_asit = "His";
                }
                else if (DNA.Substring(i, 3) == "CTA" || DNA.Substring(i, 3) == "CTC" || DNA.Substring(i, 3) == "CTT" || DNA.Substring(i, 3) == "CTG" || DNA.Substring(i, 3) == "TTA" || DNA.Substring(i, 3) == "TTG")
                {
                    amino_asit = "Leu";
                }
                else if (DNA.Substring(i, 3) == "TAC" || DNA.Substring(i, 3) == "TAT")
                {
                    amino_asit = "Tyr";
                }
                else if (DNA.Substring(i, 3) == "TTC" || DNA.Substring(i, 3) == "TTT")
                {
                    amino_asit = "Phe";
                }
                else if (DNA.Substring(i, 3) == "GAA" || DNA.Substring(i, 3) == "GAG")
                {
                    amino_asit = "Glu";
                }
                else if (DNA.Substring(i, 3) == "GAC" || DNA.Substring(i, 3) == "GAT")
                {
                    amino_asit = "Asp";
                }
                else if (DNA.Substring(i, 3) == "GTA" || DNA.Substring(i, 3) == "GTC" || DNA.Substring(i, 3) == "GTT" || DNA.Substring(i, 3) == "GTG")
                {
                    amino_asit = "Val";
                }
                else if (DNA.Substring(i, 3) == "ACA" || DNA.Substring(i, 3) == "ACC" || DNA.Substring(i, 3) == "ACT" || DNA.Substring(i, 3) == "ACG")
                {
                    amino_asit = "Thr";
                }
                else if (DNA.Substring(i, 3) == "AGA" || DNA.Substring(i, 3) == "AGG" || DNA.Substring(i, 3) == "CGA" || DNA.Substring(i, 3) == "CGC" || DNA.Substring(i, 3) == "CGT" || DNA.Substring(i, 3) == "CGG")
                {
                    amino_asit = "Arg";
                }
                else if (DNA.Substring(i, 3) == "CCA" || DNA.Substring(i, 3) == "CCC" || DNA.Substring(i, 3) == "CCT" || DNA.Substring(i, 3) == "CCG")
                {
                    amino_asit = "Pro";
                }
                else if (DNA.Substring(i, 3) == "AGC" || DNA.Substring(i, 3) == "AGT" || DNA.Substring(i, 3) == "TCA" || DNA.Substring(i, 3) == "TCC" || DNA.Substring(i, 3) == "TCT" || DNA.Substring(i, 3) == "TCG")
                {
                    amino_asit = "Ser";
                }
                else if (DNA.Substring(i, 3) == "TGC" || DNA.Substring(i, 3) == "TGT")
                {
                    amino_asit = "Cys";
                }
                else if (DNA.Substring(i, 3) == "TGG")
                {
                    amino_asit = "Trp";
                }
                else if (DNA.Substring(i, 3) == "GCA" || DNA.Substring(i, 3) == "GCC" || DNA.Substring(i, 3) == "GCT" || DNA.Substring(i, 3) == "GCG")
                {
                    amino_asit = "Ala";
                }
                else if (DNA.Substring(i, 3) == "GGA" || DNA.Substring(i, 3) == "GGC" || DNA.Substring(i, 3) == "GGT" || DNA.Substring(i, 3) == "GGG")
                {
                    amino_asit = "Gly";
                }
                else if (DNA.Substring(i, 3) == "TAA" || DNA.Substring(i, 3) == "TAG" || DNA.Substring(i, 3) == "TGA")
                {
                    amino_asit = "Stp";
                }
                else if (DNA.Substring(i, 3) == "GGA" || DNA.Substring(i, 3) == "GGC" || DNA.Substring(i, 3) == "GGT" || DNA.Substring(i, 3) == "GGG")
                {
                    amino_asit = "Gly";
                }
                else if (DNA.Substring(i, 3) == "ATG")
                {
                    amino_asit = "Met";
                }
                else
                    amino_asit = "___";

                protein = protein.Insert(i, amino_asit);

            }
            if (full_DNA_sequence != "")
            {
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("Your DNA sequence : " + full_DNA_sequence + "\n");
                Console.WriteLine("Your DNA sequence with overlap : " + overlap_DNA_sequence + "\n");
                Console.WriteLine("Protein : " + protein + "\n");

                Console.Write("\nSelect enzyme : ");
                string enzyme = Console.ReadLine();

                int final_score = 1;
                for (int j = 0; j < length; j += 3)
                {
                    if (protein.Substring(j, 3) == enzyme)
                        final_score *= 5;

                }

                if (final_score == 1)
                    final_score = 0;

                Console.WriteLine("\nYour score : " + final_score);
            }
            else
            {
                Console.SetCursorPosition(7, 5);
                Console.Write("Unfortunately you could not construct DNA sequence.. GAME OVER");
            }

        } /* End of game end function */

 



        
    }
}
