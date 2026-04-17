using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    // ============================================================
    // FlightPlan — Representa un plan de vuelo individual.
    // Almacena el identificador, la compañía aérea, las posiciones
    // (inicial, actual y destino) y la velocidad de crucero.
    // Proporciona métodos para mover el avión ciclo a ciclo y para
    // detectar conflictos con otros vuelos.
    // ============================================================
    public class FlightPlan
    {
        // ── Campos de datos ──────────────────────────────────────────────────────

        string id;                // Identificador único del vuelo (p.ej. "IB3456")
        string company;           // Compañía aérea operadora (nuevo en v2)
        Position initialPosition; // Posición de origen del vuelo (no cambia tras el inicio)
        Position currentPosition; // Posición actual durante la simulación
        Position finalPosition;   // Posición de destino que el vuelo debe alcanzar
        double velocidad;         // Velocidad de crucero en unidades/minuto

        // ── Constructores ────────────────────────────────────────────────────────

        /// <summary>
        /// Constructor v2 completo: acepta el identificador, la compañía y
        /// las coordenadas de origen, destino y velocidad.
        /// </summary>
        public FlightPlan(string id, string company, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.company = company;
            // La posición inicial e inicial coinciden al arrancar el vuelo.
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition   = new Position(fpx, fpy);
            this.velocidad       = velocidad;
        }

        /// <summary>
        /// Constructor v1 sin compañía: establece company a cadena vacía
        /// para mantener compatibilidad con código anterior.
        /// </summary>
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.company = string.Empty; // Sin compañía en v1
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition   = new Position(fpx, fpy);
            this.velocidad       = velocidad;
        }

        /// <summary>
        /// Constructor con objetos Position: compatibilidad v1 para código
        /// que ya construye las posiciones externamente.
        /// </summary>
        public FlightPlan(string id, Position initialPosition, Position currentPosition, Position finalPosition, double velocidad)
        {
            this.id = id;
            this.company = string.Empty;
            this.initialPosition = initialPosition;
            this.currentPosition = currentPosition;
            this.finalPosition   = finalPosition;
            this.velocidad       = velocidad;
        }

        /// <summary>
        /// Constructor por defecto: todos los campos a valores neutros.
        /// Útil para instanciar un vuelo vacío antes de llenarlo con datos.
        /// </summary>
        public FlightPlan()
        {
            id = string.Empty;
            company = string.Empty;
            initialPosition = new Position(0, 0);
            currentPosition = new Position(0, 0);
            finalPosition   = new Position(0, 0);
            velocidad = 0;
        }

        // ── Métodos de acceso (getters y setters) ────────────────────────────────
        // Siguen la convención del proyecto: GetX() / SetX() en lugar de propiedades.

        public string GetId() { return id; }
        public void SetId(string value) { id = value; }

        /// <summary>Devuelve la compañía aérea del vuelo.</summary>
        public string GetCompany() { return company; }

        /// <summary>Establece la compañía aérea del vuelo.</summary>
        public void SetCompany(string value) { company = value; }

        public Position GetInitialPosition() { return initialPosition; }
        public void SetInitialPosition(Position value) { initialPosition = value; }

        public Position GetCurrentPosition() { return currentPosition; }
        public void SetCurrentPosition(Position value) { currentPosition = value; }

        public Position GetFinalPosition() { return finalPosition; }
        public void SetFinalPosition(Position value) { finalPosition = value; }

        public double GetVelocidad() { return velocidad; }
        public void SetVelocidad(double value) { velocidad = value; }

        // ── Movimiento ───────────────────────────────────────────────────────────

        /// <summary>
        /// Avanza el vuelo durante el intervalo de tiempo indicado (en minutos)
        /// en línea recta hacia el destino, sin sobrepasarlo.
        /// Algoritmo:
        ///   1. Calcula la distancia a recorrer = velocidad × tiempo / 60 (si velocidad es u/h).
        ///   2. Calcula la dirección mediante la hipotenusa entre la posición actual y el destino.
        ///   3. Descompone el desplazamiento en componentes X e Y usando coseno y seno.
        ///   4. Si la nueva posición supera el destino, fija la posición en el destino.
        /// </summary>
        public void Mover(double tiempo)
        {
            // Distancia = velocidad (u/min) × tiempo (min) / 60
            // La división entre 60 convierte el tiempo a horas si la velocidad es u/h,
            // o bien ajusta la escala según la configuración del proyecto.
            double distancia = tiempo * this.velocidad / 60;

            // Calcular la dirección del vector hacia el destino.
            // La hipotenusa es la distancia euclídea entre posición actual y destino.
            double hipotenusa = Math.Sqrt(
                (finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) +
                (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));

            // Si ya está en destino (hipotenusa ≈ 0), no hay que moverse.
            if (hipotenusa == 0) return;

            // Coseno del ángulo = componente horizontal de la dirección unitaria.
            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;

            // Seno del ángulo = componente vertical de la dirección unitaria.
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            // Nueva posición = posición actual + desplazamiento en cada eje.
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            Position nextPosition = new Position(x, y);

            // Comprobar si la nueva posición se ha pasado del destino.
            // Si la distancia al siguiente punto es menor que la hipotenusa (distancia al destino),
            // el avión aún no ha llegado y se mueve a la nueva posición.
            // En caso contrario, se fija directamente en el destino para no sobrepasarlo.
            if (currentPosition.Distancia(nextPosition) < hipotenusa)
                currentPosition = nextPosition;
            else
                currentPosition = finalPosition;
        }

        /// <summary>
        /// Alias en inglés de Mover() para compatibilidad con enunciados en inglés.
        /// </summary>
        public void Move(double time)
        {
            Mover(time);
        }

        /// <summary>
        /// Reinicia el vuelo a su posición de origen.
        /// Se crea un nuevo objeto Position para no compartir referencia con initialPosition.
        /// </summary>
        public void Restart()
        {
            currentPosition = new Position(initialPosition.GetX(), initialPosition.GetY());
        }

        // ── Consultas de estado ──────────────────────────────────────────────────

        /// <summary>
        /// Devuelve true si el vuelo ha alcanzado su destino.
        /// Usa distancia < ε en lugar de comparar referencias para ser robusto
        /// frente a redondeo de punto flotante.
        /// </summary>
        public bool EstaDestino()
        {
            return currentPosition.Distancia(finalPosition) < 1e-6;
        }

        /// <summary>
        /// Alias en inglés de EstaDestino().
        /// </summary>
        public bool HasArrived()
        {
            return EstaDestino();
        }

        /// <summary>
        /// Calcula la distancia euclídea entre la posición actual de este vuelo
        /// y la posición actual del vuelo pasado como parámetro.
        /// </summary>
        public double Distance(FlightPlan plan)
        {
            return this.currentPosition.Distancia(plan.currentPosition);
        }

        /// <summary>
        /// Devuelve true si la separación actual entre este vuelo y el vuelo b
        /// es inferior a la distancia de seguridad indicada.
        /// </summary>
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            bool conclicto = false;

            // Comparar la distancia euclídea entre posiciones actuales con el umbral.
            if (this.currentPosition.Distancia(b.currentPosition) < distanciaSeguridad)
                conclicto = true;

            return conclicto;
        }

        // ── Depuración ───────────────────────────────────────────────────────────

        /// <summary>
        /// Imprime en la consola un resumen del estado actual del vuelo.
        /// Útil durante el desarrollo para verificar los valores internos.
        /// </summary>
        public void EscribeConsola()
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
