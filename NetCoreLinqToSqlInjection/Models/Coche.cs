namespace NetCoreLinqToSqlInjection.Models
{
    public class Coche : ICoche
    {
        public string Marca {  get; set; }
        public string Modelo { get; set; }
        public string Imagen {  get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMaxima { get; set; }

        public Coche ()
        {
            this.Marca = "Seat";
            this.Modelo = "Leon";
            this.Imagen = "seat.jpg";
            this.Velocidad = 0;
            this.VelocidadMaxima = 250;
        }

        public int Acelerar()
        {
            Velocidad += 20;
            if (Velocidad > VelocidadMaxima)
            {
                Velocidad = VelocidadMaxima;
            }
            return Velocidad;
        }

        public int Frenar()
        {
            Velocidad -= 20;
            if (Velocidad < 0)
            {
                Velocidad = 0;
            }
            return Velocidad;
        }
    }
}
