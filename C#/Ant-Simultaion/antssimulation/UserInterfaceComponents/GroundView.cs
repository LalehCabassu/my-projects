using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Vitruvian.Distribution;
using Vitruvian.Logging;
using Vitruvian.Services;
using Ants;

namespace UserInterfaceComponents
{
    public partial class GroundView : UserControl
    {
        #region Private Data Members
        private static Logger _logger = Logger.GetLogger(typeof(GroundView));
        private int numberOfColumns;
        private int numberOfRows;
        private float viewWidth;
        private float viewHeight;
        private float cellWidth;
        private float cellHeight;
        private float cellCenterX;
        private float cellCenterY;
        #endregion

        public event MethodInvoker PaintingFinished;

        #region Constructors
        public GroundView()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        #endregion

        #region Event Handlers

        private void GroundView_Paint(object sender, PaintEventArgs e)
        {
            _logger.Debug("Entering GroundView_Paint");

			try
			{
                Ground ground = ServiceRegistry.GetPreferredService<Ground>();
                SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();

				if (ground != null && settings != null)
				{
					RecomputeDimensions();

					DrawBorder(e.Graphics);

                    DrawFood(e.Graphics);

                    List<Colony> colonies = ServiceRegistry.GetServices<Colony>();
                    foreach (Colony colony in colonies)
					    DrawColony(e.Graphics, colony);	
				}
			}
			catch (RemoteException ex)
			{
				if (_logger.IsDebugEnabled)
					_logger.Debug(ex);
			}

            if (PaintingFinished != null)
                PaintingFinished();

            _logger.Debug("Exiting GroundView_Paint");
        }

        #endregion

        #region Private Methods

        private void RecomputeDimensions()
        {
			try
			{
                Ground ground = ServiceRegistry.GetPreferredService<Ground>();
                SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();

                if (ground != null && settings != null)
				{
					numberOfColumns = ground.Width;
					numberOfRows = ground.Height;
                    cellWidth = Convert.ToSingle((this.Width - settings.GroundBorder) / numberOfColumns);
                    cellHeight = Convert.ToSingle((this.Height - settings.GroundBorder) / numberOfRows);
					cellCenterX = Convert.ToSingle(cellWidth/2);
					cellCenterY = Convert.ToSingle(cellHeight/2);
					viewWidth = cellWidth*numberOfColumns;
					viewHeight = cellHeight*numberOfRows;
				}
			}
			catch (RemoteException ex)
			{
				if (_logger.IsDebugEnabled)
					_logger.Debug(ex);
			}
        }

        private void DrawBorder(Graphics graphics)
        {
			try
			{
                SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();
                using (Pen blackPen = new Pen(Brushes.Black, settings.GroundBorder))
				{
                    float boardCenter = settings.GroundBorder / 2;
					graphics.DrawRectangle(blackPen, boardCenter, boardCenter, viewWidth - boardCenter, viewHeight - boardCenter);
				}
			}
			catch (RemoteException ex)
			{
				if (_logger.IsDebugEnabled)
					_logger.Debug(ex);
			}
        }

        private void DrawFood(Graphics graphics)
        {
            Ground ground = ServiceRegistry.GetPreferredService<Ground>();
            SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();

            try
            {
                float squareSize = Math.Max(Convert.ToSingle(Math.Min(cellHeight, cellWidth) - 2),
                                            settings.MinSymbolSize);
                float squareHalf = Convert.ToInt32(squareSize / 2);

                for (int row = 0; row < ground.Width; row++)
                    for (int column = 0; column < ground.Height; column++)
                    {
                        Food food = ground.PeekAtFood(row, column);
                        if (food != null)
                        {
                            int foodLevel =
                                Convert.ToInt32(Math.Round(255 * (Convert.ToDouble(food.Amount) / ground.MaxFoodInAPile)));
                            Color fColor = Color.FromArgb(foodLevel, Color.DarkGreen);
                            using (SolidBrush brush = new SolidBrush(fColor))
                            {
                                graphics.FillRectangle(brush,
                                                       column * cellWidth + cellCenterX - squareHalf,
                                                       row * cellHeight + cellCenterY - squareHalf,
                                                       squareSize, squareSize);
                            }
                        }
                    }
            }
            catch (RemoteException ex)
            {
                if (_logger.IsDebugEnabled)
                    _logger.Debug(ex);
            }
        }

        private void DrawColony(Graphics graphics, Colony colony)
        {
            Ground ground = ServiceRegistry.GetPreferredService<Ground>();
            SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();

			try
			{
				Position home = colony.Home.Location;

				float squareSize = Math.Max(Convert.ToSingle(Math.Min(cellHeight, cellWidth) - 2),
                                            settings.MinSymbolSize);
				float squareHalf = Convert.ToInt32(squareSize/2);
				
				float pMarkerSize =
                    Math.Max(Convert.ToSingle(Math.Min(cellHeight, cellWidth) * settings.PheromoneRelativeMarkerSize),
                             settings.MinPheromoneMarkerSize);
				float pMarkerHalf = Convert.ToInt32(pMarkerSize/2);

                if (settings.ShowPheromone)
                {
                    for (int row = 0; row < ground.Width; row++)
                        for (int column = 0; column < ground.Height; column++)
                        {
                            PheromoneLayer layer = colony.PheromoneLayer;
                            Pheromone pheromone = layer.GetPheromone(row, column);
                            int pLevel = pheromone != null ? pheromone.Level : (int)0;

                            if (pLevel > 0)
                            {
                                if (pLevel > 255) pLevel = 255;
                                Color pColor = Color.FromArgb(pLevel, colony.ColonyColor);
                                using (SolidBrush brush = new SolidBrush(pColor))
                                {
                                    graphics.FillEllipse(brush,
                                                         column * cellWidth + cellCenterX - pMarkerHalf,
                                                         row * cellHeight + cellCenterY - pMarkerHalf,
                                                         pMarkerSize, pMarkerSize);
                                }
                            }
                        }
                }

				using (SolidBrush brush = new SolidBrush(colony.ColonyColor))
				{
					graphics.FillRectangle(brush,
					                       home.Column*cellWidth + cellCenterX - squareHalf,
					                       home.Row*cellHeight + cellCenterY - squareHalf,
					                       squareSize, squareSize);

					foreach (Ant ant in colony.Ants)
					{
						graphics.FillEllipse(brush,
						                     ant.Location.Column*cellWidth + cellCenterX - squareHalf,
						                     ant.Location.Row*cellHeight + cellCenterY - squareHalf,
						                     squareSize, squareSize);
                        graphics.DrawEllipse(Pens.Black,
                                            ant.Location.Column * cellWidth + cellCenterX - squareHalf,
                                             ant.Location.Row * cellHeight + cellCenterY - squareHalf,
                                             squareSize, squareSize);
					}
				}
			}
			catch (Exception ex)
			{
				if (_logger.IsDebugEnabled)
					_logger.Debug(ex);
			}
        }

        #endregion
    }
}
