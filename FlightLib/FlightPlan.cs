using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        // Datos que definen el plan y el estado actual del vuelo.
        string id; // identificador
        Position initialPosition;
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        double velocidad;

        // Constructores
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
        }

        public FlightPlan(string id, Position initialPosition, Position currentPosition, Position finalPosition, double velocidad)
        {
            this.id = id;
            this.initialPosition = initialPosition;
            this.currentPosition = currentPosition;
            this.finalPosition = finalPosition;
            this.velocidad = velocidad;
        }

        public FlightPlan()
        {
            id = string.Empty;
            initialPosition = new Position(0, 0);
            currentPosition = new Position(0, 0);
            finalPosition = new Position(0, 0);
            velocidad = 0;
        }

        // Métodos de acceso para poder consultar y modificar el vuelo desde la UI.
        public string GetId() { return id; }
        public void SetId(string value) { id = value; }

        public Position GetInitialPosition() { return initialPosition; }
        public void SetInitialPosition(Position value) { initialPosition = value; }

        public Position GetCurrentPosition() { return currentPosition; }
        public void SetCurrentPosition(Position value) { currentPosition = value; }

        public Position GetFinalPosition() { return finalPosition; }
        public void SetFinalPosition(Position value) { finalPosition = value; }

        public double GetVelocidad() { return velocidad; }
        public void SetVelocidad(double value) { velocidad = value; }

        // Mueve el vuelo durante el tiempo indicado sin sobrepasar su destino.

        public void Mover(double tiempo)
        {
            // Calcula la distancia recorrida con la velocidad actual.
            double distancia = tiempo * this.velocidad / 60;

            // Obtiene la dirección del desplazamiento hacia el destino.
            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            if (hipotenusa == 0) return; // Ya está en destino
            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            // Calcula la nueva posición siguiendo la trayectoria.
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            Position nextPosition = new Position(x, y);

            // Si el siguiente paso alcanza o supera el destino, fija la posición final.
            if (currentPosition.Distancia(nextPosition) < hipotenusa)
                currentPosition = nextPosition;
            else
                currentPosition = finalPosition;
        }

        // Alias en inglés para mantener compatibilidad con los enunciados
        public void Move(double time)
        {
            Mover(time);
        }

        public void Restart()
        {
            // Reinicia el vuelo a su posición de origen.
            currentPosition = new Position(initialPosition.GetX(), initialPosition.GetY());
        }

        // Indica si el vuelo ya ha alcanzado el destino.
        public bool EstaDestino()
        {
            // Usar distancia evita depender de si ambas posiciones son la misma referencia.
            return currentPosition.Distancia(finalPosition) < 1e-6;
        }

        // Método público HasArrived
        public bool HasArrived()
        {
            return EstaDestino();
        }

        // Calcula la distancia actual a otro vuelo.
        public double Distance(FlightPlan plan)
        {
            return this.currentPosition.Distancia(plan.currentPosition);
        }

        // Detecta conflicto cuando la separación es menor que la distancia de seguridad.
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            bool conclicto = false;

            if (this.currentPosition.Distancia(b.currentPosition) < distanciaSeguridad)
                conclicto = true;

            return conclicto;
        }

        public void EscribeConsola()
        // Escribe en consola un resumen del estado del vuelo.
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            Console.WriteLine("Velocidad: {0:F2}", velocidad);
            Console.WriteLine("Posición actual: ({0:F2}, {1:F2})", currentPosition.GetX(), currentPosition.GetY());
            if (this.EstaDestino())
                Console.WriteLine("Ha llegado al destino");
            Console.WriteLine("******************************");
        }
    }
}
