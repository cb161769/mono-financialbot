

namespace mono_financialbot_backend_cs_external_serivces_integration.Providers.Stocks.Service
{
    public class StockService : IBot
    {
        private ILogger<StockService> _logger;
        public StockService(ILogger<StockService> logger)
        {
            _logger=logger;
        }
        public StockModel GetStockQuote(RabbitMQMessage message)
        {
            try
            {
                _logger.LogInformation("initializing method to get stock code");
                if (message == null)
                {
                    _logger.LogError($"Fail the argument {nameof(message)} cannot be null.");

                    throw new ArgumentNullException($"Fail the argument {nameof(message)} cannot be null.");
                }
                else
                {
                    _logger.LogInformation("proceed to extract message from request ");
                    string code = GetStock(message.Message);
                    if (!string.IsNullOrEmpty(code))
                    {
                        IList<StockModel> stockQuotes = GetStocks(code);
                        if (stockQuotes != null && stockQuotes.Any())
                        {
                            _logger.LogInformation("proceed to return stockQuotes from api to [RabbitMQ]");
                            return stockQuotes[0];

                        }
                        else
                        {
                            _logger.LogWarning("proceed to return empty stockQuotes because the provided code was null");
                            return null;

                        }
                    }
                    else
                    {
                        _logger.LogWarning("proceed to return empty stockQuotes because the provided code was null");

                        return null;
                    }

                }

            }
            catch (Exception e)
            {
                _logger.LogError("an internal error has occured when get stock code from event, details:", e.ToString());

                throw new FormatException(e.Message);
            }
        }
        private string GetStock(string code)
        {
            try
            {
                _logger.LogInformation("initializing method to get stock code");
                var stockCode = string.Empty;
                var stockProcessor = new Regex(@"\/stock=(?<StockCode>.*)");
                Match occurencies = stockProcessor.Match(code);
                if (occurencies.Success)
                {
                    _logger.LogInformation("[stock command was found, proceed to extract code]");
                    stockCode = occurencies.Groups["code"].Value;

                }
                return stockCode;
            }
            catch (Exception e )
            {
                _logger.LogError("an internal error has occured when get stock code, details:", e.ToString());
                throw new Exception(e.Message);
            }
        }
        private IList<StockModel> GetStocks(string code)
        {
            try
            {
                _logger.LogInformation("initializing method to get stocks from related code");
                var request = (HttpWebRequest)WebRequest.Create($"https://stooq.com/q/l/?s={code}&f=sd2t2ohlcv&h&e=csv");
                request.Method = "GET";
                _logger.LogInformation("send request to get get stocks from related code to stook api");

                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation("[stooqapi] got response to get get stocks  succesfully");


                    TextReader reader = new StreamReader(response.GetResponseStream());
                    var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                    var records = csvReader.GetRecords<StockModel>().ToList();
                    _logger.LogInformation("[cvs] records obtained successfully");
                    return records;

                }
                else
                {
                    _logger.LogError($"[stooqapi] could not obtain response from serverm, details {response}");
                    throw new Exception(response.ToString());
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.ToString());
            }
        }
    }
}
