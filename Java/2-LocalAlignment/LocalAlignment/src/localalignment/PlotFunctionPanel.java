/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package localalignment;
import javax.swing.JPanel;
import java.awt.*;

/**
 *
 * @author Tulip
 */

public class PlotFunctionPanel extends JPanel {
    int size;
    static double maxValue;
    double [] xAxis, yAxis;

    public PlotFunctionPanel(int s, double [] xa, double [] ya){
            size = s;
            maxValue = ya[ya.length - 1];
            xAxis = new double [4];
            yAxis = new double [4];
            xAxis = xa;
            yAxis = ya;
            setPreferredSize(new Dimension(size, size));
    }

    public void paintComponent(Graphics g){
            g.drawLine(10, 10, 10, size - 10);
            g.drawLine(10, size - 10, size - 10, size - 10);
            int x = (size - 10) / 4, y = (size - 10) / 4, vA = 4;

            g.setFont(new Font("Sansserif", Font.BOLD, size/10));
            for(int i = 0; i < 4; i++){
                g.drawLine(x, y+5, x, y-5);
                
                
                /*if(i != 4 && vA != 4){
                        if(i > 4)
                                g.drawString(tick[i]+"", x-size/40, y+size/21);
                        else
                                g.drawString(tick[i]+"", x-size/30, y+size/21);
                        if(vA > 4)
                                g.drawString(tick[vA]+"", y-size/13, x+size/60);
                        else
                                g.drawString(tick[vA]+"", y-size/12, x+size/60);
                }
                 *
                 */
                g.drawLine(y+5, x, y-5, x);
                x+=size/10;
                vA--;
            }
            g.setColor(Color.RED);
            
            //Log-Log Plot

            double min = 0, max = maxValue, ratio = size/max, fx;
            for(int i = 0; i < xAxis.length; i++){
                //preventing round-off error
                min = Math.round(xAxis[i] * 1000.0) / 1000.0;
                fx = Math.round(yAxis[i] * 1000.0) / 1000.0;
                g.drawLine((int)(size / 2 + (ratio * min)), (int)(size / 2 - (ratio * fx)),
                                (int)(size / 2 + (ratio * min)), (int)(size /2 - (ratio * fx)));
            }


            /*
            double min = (-maxValue), max = maxValue, ratio = size/(max*2), fx;
            for(; min <= max; min += 0.025){
                    //preventing round-off error
                    min = Math.round(min*1000.0)/1000.0;
                    fx=Math.round((min*min*min)*1000.0)/1000.0;
                    g.drawLine((int)(size/2+(ratio*min)), (int)(size/2-(ratio*fx)),
                                    (int)(size/2+(ratio*min)), (int)(size/2-(ratio*fx)));
            }
             *
             */

    }

    //finds the values of the ticks on the axis e.g. -2.0, -1.5, -1.0, -0.5, 0.0, etc
    private static double[] getTicks(){
            double increment = maxValue / 5, currentTick = -1*(maxValue);
            double[] tick = new double[9];
            for(int i = 0; i < 9; i++){
                    currentTick+=increment;
                    tick[i] = Math.round(currentTick*100.0)/100.0;
            }
            return tick;
    }
}
