using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class Position
    {
        // Coordenadas cartesianas del punto.
        double x; // coordenada X (2D)
        double y; // coordenada Y (2D)

        // Constructores

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Métodos básicos de acceso y cálculo.

        public double GetX()
        // getter del atributo x
        { return x; }

        public double GetY()
        // getter del atributo y
        { return y; }

        public double Distancia(Position b)
        // Devuelve la distancia euclídea entre esta posición y otra.
        {
            double resultado = Math.Sqrt((x - b.x) * (x - b.x) + (y - b.y) * (y - b.y));
            return resultado;
        }
    }
}
