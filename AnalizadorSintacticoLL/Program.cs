using System;
using System.Numerics;

public class SyntaxAnalyzer
{
    private int posicion;
    private string input;

    public void Analiza(string expression)
    {
        input = expression.Replace(" ", ""); // Eliminar los espacios en blanco
        posicion = 0;

        Console.WriteLine("Iniciando análisis sintáctico...");

        if (Expr() && Seguir() == "$")
        {
            Console.WriteLine("Análisis sintáctico exitoso.");
        }
        else
        {
            Console.WriteLine("Error de análisis sintáctico en la posición " + posicion + ".");
        }
    }
    private void SiguienteSimbolo()
    {
        if (posicion < input.Length)
        {
            posicion++;
        }
    }

    private string Seguir()
    {
        if (posicion < input.Length)
        {
            return input[posicion].ToString();
        }
        else
        {
            return "$";
        }
    }

    private bool Expr()
    {
        if (Term() && RestExpr())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool RestExpr()
    {
        if (Seguir() == "+")
        {
            SiguienteSimbolo();
            return Term() && RestExpr();
        }
        else if (Seguir() == "-")
        {
            SiguienteSimbolo();
            return Term() && RestExpr();
        }
        else
        {
            return true;
        }
    }

    private bool Term()
    {
        if (Factor() && RestTerm())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool RestTerm()
    {
        if (Seguir() == "*")
        {
            SiguienteSimbolo();
            return Factor() && RestTerm();
        }
        else if (Seguir() == "/")
        {
            SiguienteSimbolo();
            return Factor() && RestTerm();
        }
        else
        {
            return true;
        }
    }

    private bool Factor()
    {
        if (Seguir() == "(")
        {
            SiguienteSimbolo();
            if (Expr() && Seguir() == ")")
            {
                SiguienteSimbolo();
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (IsNumber(Seguir()))
        {
            SiguienteSimbolo();
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsNumber(string symbol)
    {
        int number;
        return int.TryParse(symbol, out number);
    }

    public static void Main(string[] args)
    {
        SyntaxAnalyzer analyzer = new SyntaxAnalyzer();
        int salir = 0;
        do
        {
            System.Console.Clear();
            Console.Write("Ingrese una expresión aritmética: ");
            string expression = Console.ReadLine();

            analyzer.Analiza(expression);

            Console.WriteLine("Desea salir del programa?\n");
            Console.WriteLine("0->No\n1->Si, quiero salir.");
            salir = Int32.Parse((Console.ReadLine()));

        } while (salir == 0) ;
        Console.WriteLine("Terminando programa...");
        Console.WriteLine("Presiona cualquier tecla para terminar");
        Console.ReadKey();
    }
}