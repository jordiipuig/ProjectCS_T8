using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        // Contenedor sencillo para los vuelos activos de la simulación.
        FlightPlan[] vector = new FlightPlan[10];
        int number = 0;

        public int getnum()
        {
            return number;
        }
        

        public int AddFlightPlan(FlightPlan p)
        {
            // Inserta un vuelo al final si todavía queda capacidad.
            if (number == 10)
            { return -1; }
            else
            {
                vector[number] = p;
                number++;
                return 0;
            }
        }

        public FlightPlan GetFlightPlan(int i)
        {
            // Devuelve el vuelo pedido o null si el índice no existe.
            if (i < 0 || i >= number)
            { return null; }
            else
            {
                return vector[i];
            }


        }
        public void Mover(double tiempo)
        {
            // Avanza todos los vuelos el mismo tiempo de simulación.
            int i = 0;
            while (i < number)
            {
                vector[i].Mover(tiempo);
                i++;
            }

        }
        public void Clear()
        {
            // Vacía por completo la colección de vuelos cargados.
            for (int i = 0; i < number; i++)
                vector[i] = null;
            number = 0;
        }

        public void EscribeConsola()
        {
            // Muestra por consola el estado de todos los vuelos almacenados.
            int i = 0;
            while (i < number)
            {
                vector[i].EscribeConsola();
                i++;
            }

        }
    }
}
