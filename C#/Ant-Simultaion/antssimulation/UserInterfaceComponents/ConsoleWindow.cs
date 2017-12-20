using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vitruvian.Serialization;
using Vitruvian.Services;

namespace AntsUI
{
    [OptimisticSerialization]
    public partial class ConsoleWindow : Form
    {
        public ConsoleWindow()
        {
            InitializeComponent();            
        }

        private void ServiceAdded(IService service)
        {
            BeginInvoke(new MethodInvoker(RefreshServices));
        }

        private void ServiceRemoved(IService service)
        {
            BeginInvoke(new MethodInvoker(RefreshServices));
        }

        private void RefreshServices()
        {
            objectBrowser1.SetObject("Services", ServiceRegistry.Services);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ServiceRegistry.ServiceAdded += ServiceAdded;
            ServiceRegistry.ServiceRemoved += ServiceRemoved;
            RefreshServices();

            /*
            try
            {
                Ground ground = ServiceRegistry.GetPreferredService<Ground>();
                int width = ground.Width;
                int height = ground.Height;

                int maxFood = ground.MaxFoodInAPile;
                int numFoodPiles = ground.NumberOfFoodPiles;
                
                List<Food> piles = new List<Food>();
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Food food = ground[(int)j, (int)i];
                        if (food != null)
                            piles.Add(food);
                    }
                }

                List<Food> extracted = new List<Food>();
                for (int i = 0; i < piles.Count; i++)
                {
                    Food food = piles[i].Extract((int)(piles[i].Amount / 2));
                    extracted.Add(food);
                }
                
                Colony colony = ServiceRegistry.GetPreferredService<Colony>();
                int numAnts = colony.Ants.Count;

                int pWidth = colony.PheromoneLayer.Width;
                int pHeight = colony.PheromoneLayer.Height;

                objectBrowser1.SetObject("Colony", colony);
            }
            catch (Exception ex)
            {
            }
            */
        }
    }
}