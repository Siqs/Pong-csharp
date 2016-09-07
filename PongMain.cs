//Name: Carlos Augusto Potter Neto
//Name: Lucas Siqueira
//Course ID: CPSC223N
//Assignment 6

using System;
using System.Windows.Forms;  //Needed for "Application" on next to last line of Main
public class PongMain
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("Welcome to the Main method of the assignment 3.");
        PongInterface pong = new PongInterface();
        Application.Run(pong);
        System.Console.WriteLine("Main method will now shutdown.");
    }
}
