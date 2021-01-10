using amadeus;
using amadeus.util;
using Migrations;
using Migrations.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AirTickets.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public static void GetLocations()
        {
            try
            {

                var apikey = "REPLACE_BY_YOUR_API_KEY";
                var apisecret = "REPLACE_BY_YOUR_API_SECRET";

                Configuration amadeusconfig = Amadeus.builder(apikey, apisecret);
                amadeusconfig.setLoglevel("debug");
                Amadeus amadeus = amadeusconfig.build();

                Console.WriteLine("Get CheckinLink:");
                amadeus.resources.CheckinLink[] checkinLinks = amadeus.referenceData.urls.checkinLinks.get(Params
                        .with("airlineCode", "BA"));

                Console.WriteLine(AmadeusUtil.ArrayToStringGeneric(checkinLinks, "\n"));

                Console.WriteLine("\n\n");

                Console.WriteLine("Get Locations:");

                amadeus.resources.Location[] locations = amadeus.referenceData.locations.get(Params
                    .with("keyword", "LON")
                    .and("subType", resources.referenceData.Locations.ANY));

                Console.WriteLine(AmadeusUtil.ArrayToStringGeneric(locations, "\n"));

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //using (DatabaseContext context = new DatabaseContext())
            //{
            //    context.Users.Add(new User() { ID = 0, Name = "USer1" });
            //    context.SaveChanges();
            //}

            GetLocations();
        }
    }
}