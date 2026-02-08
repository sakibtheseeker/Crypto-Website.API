namespace Crypto_Website.Application.DTO.Portfolio
{
    public class PortfolioResponseDto
    {
        public int Pid { get; set; }
        public List<PortfolioAssetResponseDto> Assets { get; set; }
    }
}
