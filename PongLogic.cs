//Name: Carlos Augusto Potter Neto
//Name: Lucas Siqueira
//Course ID: CPSC223N
//Assignment 6

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PongLogic
{
    public static int verticaldelta(int angle)
    {
        double rad = (Math.PI * angle) / 180;
        double dy = Math.Tan(rad) * 5;
        int delta = (int)Math.Round(dy, 0, MidpointRounding.AwayFromZero);

        return delta;
    }
}
