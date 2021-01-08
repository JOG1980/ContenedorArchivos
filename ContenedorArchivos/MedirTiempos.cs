using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Diagnostics;

namespace ContenedorArchivos
{
    public class MedirTiempos
    {

        Stopwatch stopWatch;
        String nombre_elemento = "";

        public MedirTiempos(String nombre_elemento)
        {
            this.nombre_elemento = nombre_elemento;
            stopWatch = new Stopwatch();
        }

        public void start()
        {
            stopWatch.Start();
        }
        public void end()
        {
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.WriteLine("> Tiempo de ejecucion de " + nombre_elemento + ": " + elapsedTime);
        }
    }
}