using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vitruvian.Serialization.Formatters;

namespace Ants
{
    public class PheromoneLayerFormatter :  Formatter
    {
        /// <summary>
        /// Format the object into a string.
        /// </summary>
        /// <param name="value">The object to convert into a string.</param>
        /// <returns>A string representing the object.</returns>
        override public string Format(object value)
        {
            string result = string.Empty;
            PheromoneLayer pheromoneLayer = (PheromoneLayer)value;
            if (pheromoneLayer != null)
            {
                for (int row = 0; row < pheromoneLayer.Height; row++)
                    for (int column = 0; column < pheromoneLayer.Width; column++)
                    {
                        Pheromone pheromone = pheromoneLayer.GetPheromone(row, column);
                        if (pheromone!=null)
                        {
                            if (result != string.Empty)
                                result += "|";
                            result += string.Format("{0},{1},{2}", row, column, pheromone.Level);
                        }
                    }
            }
            return result;
        }

        /// <summary>
        /// Create an object from the string.
        /// </summary>
        /// <param name="value">The string representing the object.</param>
        /// <returns>The object that was represented by the string.</returns>
        override public object Unformat(string value)
        {
            PheromoneLayer result = new PheromoneLayer();

            if (string.IsNullOrEmpty(value))
            {
                string[] pCells = value.Split('|');
                foreach (string p in pCells)
                {
                    try
                    {
                        string[] pValues = p.Split(',');
                        if (pValues.Length == 3)
                        {
                            Pheromone pheromone = new Pheromone(Convert.ToInt32(pValues[2]));
                            result.SetPheromone(new Position(Convert.ToInt32(pValues[0]), Convert.ToInt32(pValues[1])), pheromone);
                        }
                    }
                    catch { }
                }
            }

            return result;
        }

    }
}
