namespace WebApplication1
{
    public class Dtos
    {
        public record BasicDto(Guid Azonosito, int Ertekeles, string Leiras, string LetrehozasIdeje);
        public record CreateDto(int Ertekeles, string Leiras);
        public record UpdateDto();
        public record DeleteDto();
    }
}
