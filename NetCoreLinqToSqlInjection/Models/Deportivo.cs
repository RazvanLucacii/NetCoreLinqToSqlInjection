namespace NetCoreLinqToSqlInjection.Models
{
    public class Deportivo: ICoche
    {
        public Deportivo() 
        {
            this.Marca = "Audi";
            this.Modelo = "R8";
            this.Imagen = "r8.jpg";
            this.Velocidad = 0;
            this.VelocidadMaxima = 330;
        }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }

        public int Acelerar()
        {
            this.Velocidad += 60;
            if(this.Velocidad >= this.VelocidadMaxima)
            {
                this.Velocidad = this.VelocidadMaxima;
            }
            return this.Velocidad;
        }

        public int Frenar()
        {
            Velocidad -= 30;
            if (Velocidad < 0)
            {
                Velocidad = 0;
            }
            return Velocidad;
        }
    }
}
