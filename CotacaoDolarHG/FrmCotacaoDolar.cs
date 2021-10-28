using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Windows.Forms;

namespace CotacaoDolarHG
{
    public partial class FrmCotacaoDolar : Form
    {
        public FrmCotacaoDolar()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnSearch_ClickAsync(object sender, EventArgs e)
        {
            string BaseUrl = "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=1aab87d4";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(BaseUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;

                        Market market = JsonConvert.DeserializeObject<Market>(result);
                        LblBuy.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Buy);
                        LblSell.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Sell);
                        LblVar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:P}", market.Currency.Variation / 100);
                    }
                    else
                    {
                        LblBuy.Text = "-";
                        LblSell.Text = "-";
                        LblVar.Text = "-";
                    }
                }
                catch (Exception ex)
                {
                    LblBuy.Text = "-";
                    LblSell.Text = "-";
                    LblVar.Text = "-";

                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
