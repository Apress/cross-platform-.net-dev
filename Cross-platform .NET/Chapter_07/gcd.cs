//Filename:	gcd.cs 
//Purpose:	Implements greatest common denominator and lowest common multiple algorithms.
//		Handy for getting good marks in pesky beginners programming assignments.
//Notes:	This doesn't appear in the book but quit complaining... free is free, damn it!

using System;

public class math
{
    static void Main(string[] args)
    {
        System.Console.WriteLine(lcm(Int32.Parse(args[0]), Int32.Parse(args[1])));
    }

    public static int gcd(int a, int b)
    {
        while (b != 0)
        {
            int c = a % b;
            a = b;
            b = c;
        }
        
        return a;
    }
    
    public static int lcm(int a, int b)
    {
        return (a * b) / gcd(a, b);
    }
}