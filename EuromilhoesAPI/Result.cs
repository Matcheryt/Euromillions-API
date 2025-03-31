using EuromilhoesAPI.Context.Models;

namespace EuromilhoesAPI
{
    public struct Result
    {
        public Result(PrizeDraw prizeDraw)
        {
            Date = prizeDraw.Date;
            Numbers = new[] {prizeDraw.Ball1, prizeDraw.Ball2, prizeDraw.Ball3, prizeDraw.Ball4, prizeDraw.Ball5};
            Stars = new[] {prizeDraw.Star1, prizeDraw.Star2};
            ResultPrettified =
                $"{prizeDraw.Ball1} {prizeDraw.Ball2} {prizeDraw.Ball3} {prizeDraw.Ball4} {prizeDraw.Ball5} + {prizeDraw.Star1} {prizeDraw.Star2}";
        }

        public string Date { get; set; }

        public short[] Numbers { get; set; }

        public short[] Stars { get; set; }

        public string ResultPrettified { get; set; }
    }
}
