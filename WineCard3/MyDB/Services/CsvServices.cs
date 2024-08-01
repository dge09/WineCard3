using System.Collections.Generic;
using WineCard3.MyDB.DTOs;
using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB.Services
{
    public class CsvServices
    {
        private List<Style> dbStyles = new();
        private List<Origin> dbOrigins = new();
        private List<Wine> dbWines = new();
        private List<Card> dbCards = new();

        public async Task SecureCsvDataExistancyInDb(List<String> csvLines)
        {
            List<CsvDto> csvDtos = GetCsvDtos(csvLines);
            await SecureExistanceStyle(csvDtos);
            await SecureExistanceOrigin(csvDtos);
            await SecureExistanceWine(csvDtos);
            await SecureExistanceCard(csvDtos);

            await ConnectCardsWithWines(csvDtos);
        }

        private List<CsvDto> GetCsvDtos(List<String> csvLines)
        {
            List<CsvDto> csvDtos = new List<CsvDto>();

            foreach (var csvLine in csvLines)
            {
                List<string> csvLineElements = csvLine.Split(',').ToList();

                CsvDto newCsvDto = new();

                newCsvDto.CardName = csvLineElements[0];
                newCsvDto.CardDscp = csvLineElements[1];
                newCsvDto.WineName = csvLineElements[2];
                newCsvDto.Style = csvLineElements[3];
                newCsvDto.SweetnessLevel = int.Parse(csvLineElements[4]);
                newCsvDto.Year = int.Parse(csvLineElements[5]);
                newCsvDto.Rating = float.Parse(csvLineElements[6]);
                newCsvDto.Price = float.Parse(csvLineElements[7]);
                newCsvDto.Origin = csvLineElements[8];

                csvDtos.Add(newCsvDto);
            }

            return csvDtos;
        }

        private async Task SecureExistanceStyle(List<CsvDto> csvDtos)
        {
            List <Style> dbStyles = await MySer.styleServices.GetAllAsync();
            List<string> csvStyles = new List<string>();

            foreach (CsvDto csvDto in csvDtos)
            {
                if(csvDto.Style != null && !csvStyles.Exists(x => x == csvDto.Style))
                {
                    csvStyles.Add(csvDto.Style);
                }
            }

            foreach (string csvStyle in csvStyles)
            {
                if(!dbStyles.Exists(x => x.StyleDscp == csvStyle))
                {
                    Style newStyle = new Style();
                    newStyle.StyleDscp = csvStyle;

                    await MySer.styleServices.CreateAsync(newStyle);
                }
            }
        }

        private async Task SecureExistanceOrigin(List<CsvDto> csvDtos)
        {
            List<Origin> dbOrigins = await MySer.originServices.GetAllAsync();
            List<string> csvOrigins = new List<string>();

            foreach (CsvDto csvDto in csvDtos)
            {
                if (csvDto.Origin != null && !csvOrigins.Exists(x => x == csvDto.Origin))
                {
                    csvOrigins.Add(csvDto.Origin);
                }
            }

            foreach (string csvOrigin in csvOrigins)
            {
                if (!dbOrigins.Exists(x => x.OriginName == csvOrigin))
                {
                    Origin newOrigin = new Origin();
                    newOrigin.OriginName = csvOrigin;

                    await MySer.originServices.CreateAsync(newOrigin);
                }
            }
        }

        private async Task SecureExistanceWine(List<CsvDto> csvDtos)
        {
            await UpdateLists();
            List<Wine> csvWines = new List<Wine>();

            foreach (CsvDto csvDto in csvDtos)
            {
                Wine w = await GetWineFromCsvDto(csvDto);

                if (!WineExistanceInList(csvWines,w))
                {
                    csvWines.Add(w);
                }
            }

            foreach (Wine csvWine in csvWines)
            {
                if (!WineExistanceInList(dbWines,csvWine))
                {   
                    await MySer.wineServices.CreateAsync(csvWine);
                }
            }
        }

        private async Task SecureExistanceCard(List<CsvDto> csvDtos)
        {
            await UpdateLists();
            List<Card> csvCards = new List<Card>();

            foreach (CsvDto csvDto in csvDtos)
            {
                Card c = await GetCardFromCsvDto(csvDto);

                if (!csvCards.Exists(x => x.CardName == c.CardName &&
                                          x.CardDscp == c.CardDscp ))
                {
                    csvCards.Add(c);
                }
            }

            foreach (Card csvCard in csvCards)
            {
                if (!dbCards.Exists(x => x.CardName == csvCard.CardName &&
                                          x.CardDscp == csvCard.CardDscp))

                    await MySer.cardServices.CreateAsync(csvCard);
                }
            }

        private async Task ConnectCardsWithWines(List<CsvDto> csvDtos)
        {
            await UpdateLists();

            foreach (Card c in dbCards)
            {
                foreach (CsvDto csvDto in csvDtos)
                {
                    if (c.CardName == csvDto.CardName)
                    {
                        Wine w = dbWines.Where(x => x.WineName == csvDto.WineName &&
                                                    x.Style.StyleDscp == csvDto.Style &&
                                                    x.SweetnessLevel == csvDto.SweetnessLevel &&
                                                    x.Year == csvDto.Year &&
                                                    x.Rating == csvDto.Rating &&
                                                    x.Price == csvDto.Price &&
                                                    x.Origin.OriginName == csvDto.Origin).FirstOrDefault();

                        c.Wines.Add(w);
                        await MySer.cardServices.UpdateAsync(c);
                    }
                }
            }
        }

        private async Task<Wine> GetWineFromCsvDto(CsvDto csvDto)
        {
            Wine w = new();
            await UpdateLists();

            w.WineName = csvDto.WineName;
            w.Style = dbStyles.Where(x => x.StyleDscp == csvDto.Style).FirstOrDefault();
            w.StyleID = w.Style.ID;
            w.SweetnessLevel = csvDto.SweetnessLevel;
            w.Year = csvDto.Year;
            w.Rating = csvDto.Rating;
            w.Price = csvDto.Price;
            w.Origin = dbOrigins.Where(x => x.OriginName == csvDto.Origin).FirstOrDefault();
            w.OriginID = w.Origin.ID;

            return w;
        }

        private async Task<Card> GetCardFromCsvDto(CsvDto csvDto)
        {
            Card c = new();
            await UpdateLists();

            c.CardName = csvDto.CardName;
            c.CardDscp = csvDto.CardDscp;

            return c;
        }

        public bool WineExistanceInList(List<Wine> referenceList,Wine w)
        {
            return referenceList.Exists(x => x.WineName == w.WineName &&
                                             x.Style.StyleDscp == w.Style.StyleDscp &&
                                             x.SweetnessLevel == w.SweetnessLevel &&
                                             x.Year == w.Year &&
                                             x.Rating == w.Rating &&
                                             x.Price == w.Price &&
                                             x.Origin.OriginName == w.Origin.OriginName);
        }

        public bool CardExistanceInList(List<Card> referenceList,Card c)
        {
            return referenceList.Exists(x => x.CardName == c.CardName &&
                                             x.CardDscp == c.CardDscp);
        }

        private async Task UpdateLists()
        {
            dbStyles = await MySer.styleServices.GetAllAsync();
            dbOrigins = await MySer.originServices.GetAllAsync();
            dbWines = await MySer.wineServices.GetAllAsync();
            dbCards = await MySer.cardServices.GetAllAsync();
        }
    }
}