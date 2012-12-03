using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Text;

namespace BurnDown.Models
{
    public class projectChart
    {
        private BurnDown.Models.IChartable[] _chartEntities;
        StringBuilder jsOutput;

        public projectChart(IChartable[] projectTasks)
        {
            _chartEntities = projectTasks;
            jsOutput = new StringBuilder();

        }

        private void clickDetection(Dictionary<string, int>[] coordinates, string canvasName)
        {
            jsOutput.AppendLine("    function getMousePos(canvas, evt){");
            jsOutput.AppendLine("    // get canvas position");
            jsOutput.AppendLine("    var obj = canvas;");
            jsOutput.AppendLine("    var top = 0;");
            jsOutput.AppendLine("    var left = 0;");
            jsOutput.AppendLine("    while (obj && obj.tagName != 'BODY') {");
            jsOutput.AppendLine("    top += obj.offsetTop;");
            jsOutput.AppendLine("    left += obj.offsetLeft;");
            jsOutput.AppendLine("    obj = obj.offsetParent;");
            jsOutput.AppendLine("    }");
            jsOutput.AppendLine("    // return relative mouse position");
            jsOutput.AppendLine("    var mouseX = evt.clientX - left + window.pageXOffset;");
            jsOutput.AppendLine("    var mouseY = evt.clientY - top + window.pageYOffset;");
            jsOutput.AppendLine("    return {");
            jsOutput.AppendLine("    x: mouseX,");
            jsOutput.AppendLine("    y: mouseY");
            jsOutput.AppendLine("    };");
            jsOutput.AppendLine("    }");

            jsOutput.AppendLine("window.onload = function(){");
            jsOutput.AppendLine("  var canvas = document.getElementById('" + canvasName + "');");
            jsOutput.AppendLine("   var context = canvas.getContext('2d');");

            jsOutput.AppendLine("   canvas.addEventListener('click', function(evt){");
            jsOutput.AppendLine("   var coord = getMousePos(canvas, evt);");
            jsOutput.AppendLine("   var x = coord.x;");
            jsOutput.AppendLine("   var y = coord.y;");

            for (int i = 0; i < coordinates.Count(); i++)
            {

                jsOutput.AppendLine("if(" + coordinates[i]["x0"] + "< x && x <" + coordinates[i]["x1"] + " && y > " + coordinates[i]["y"] + "){");
                jsOutput.AppendLine("window.location.href = '" + ("Task/" + _chartEntities[i].id) + "'}");



            }



            jsOutput.AppendLine("  }, false);");
            jsOutput.AppendLine("};");

        }

        public string createTasksChart(string canvasName, int canvasHeight, int canvasWidth)
        {
            int ch = canvasHeight - 20;
            Dictionary<string, int>[] taskCoordinates = new Dictionary<string, int>[_chartEntities.Count()];

            jsOutput.Append("var c = document.getElementById(\"" + canvasName + "\");");
            jsOutput.AppendLine("var ctx = c.getContext(\"2d\");");
            jsOutput.AppendLine("ctx.moveTo(0," + ch / 2 + ");");
            jsOutput.AppendLine("ctx.lineTo(" + canvasWidth + "," + ch / 2 + ");");
            jsOutput.AppendLine("ctx.moveTo(0," + ch + ");");
            jsOutput.AppendLine("ctx.lineTo(" + canvasWidth + "," + ch + ");");
            jsOutput.AppendLine("ctx.stroke();");
            jsOutput.AppendLine(" ctx.strokeText('50%',0," + ch / 2 + ");");
            int barWidth = canvasWidth / ((_chartEntities.Count() * 2) + 1);

            for (int i = 0; i < _chartEntities.Count(); i++)
            {
                int x = barWidth + (barWidth * i) * 2;
                if (_chartEntities.Count() == 1)
                {
                    x = canvasWidth / 3;
                }
                int h = _chartEntities[i].percentCompleted * ch / 100;
                jsOutput.AppendLine("var ctx" + i + "= c.getContext(\"2d\");");
                jsOutput.AppendLine("ctx" + i + ".fillStyle ='rgb(0,0,55)';");
                if (h == 0) h = 2;
                jsOutput.AppendLine("ctx" + i + ".fillRect(" + x + ",(" + (ch - h) + ")," + barWidth + "," + h + ");");
                jsOutput.AppendLine("ctx" + i + ".shadowColor=\"black\";");
                jsOutput.AppendLine("ctx" + i + ".shadowBlur =3;");
                jsOutput.AppendLine("ctx" + i + ".shadowOffsetX=3;");
                jsOutput.AppendLine("ctx" + i + ".shadowOffsetY=-3;");
                // jsOutput.AppendLine("ctx" + i + ".fill();");
                jsOutput.AppendLine("ctx.strokeText(\"" + _chartEntities[i].id + "\"," + (x + barWidth / 2) + "," + (canvasHeight - 10) + ");");
                Dictionary<string, int> tc = new Dictionary<string, int>();
                tc.Add("x0", x);
                tc.Add("x1", x + barWidth);
                tc.Add("y", ch - h);

                taskCoordinates[i] = tc;

            }

            clickDetection(taskCoordinates, canvasName);



            return jsOutput.ToString();
        }



        public string createProjectsChart(string canvasName, int canvasHeight, int canvasWidth)
        {
            int ch = canvasHeight - 20;
            Dictionary<string, int>[] projectCoordinates = new Dictionary<string, int>[_chartEntities.Count()];





            jsOutput.Append("var c = document.getElementById(\"" + canvasName + "\");");
            jsOutput.AppendLine("var ctx = c.getContext(\"2d\");");
            jsOutput.AppendLine("ctx.moveTo(0," + ch / 2 + ");");
            jsOutput.AppendLine("ctx.lineTo(" + canvasWidth + "," + ch / 2 + ");");
            jsOutput.AppendLine("ctx.moveTo(0," + ch + ");");
            jsOutput.AppendLine("ctx.lineTo(" + canvasWidth + "," + ch + ");");
            jsOutput.AppendLine("ctx.stroke();");
            jsOutput.AppendLine("ctx.moveTo(0," + ch / 2 + ");");
            jsOutput.AppendLine("ctx.Text(0," + ch / 2 + ");");


           jsOutput.AppendLine(" ctx.textBaseline = 'right';");
           jsOutput.AppendLine(" ctx.strokeText('50%',0," + ch / 2 + ");");




            int barWidth = canvasWidth / ((_chartEntities.Count() * 2) + 1);
            for (int i = 0; i < _chartEntities.Count(); i++)
            {
                int x = barWidth + (barWidth * i) * 2;
                if (_chartEntities.Count() == 1)
                {
                    x = canvasWidth / 3;
                }
                int h = _chartEntities[i].percentCompleted * ch / 100;
                jsOutput.AppendLine("var ctx" + i + "= c.getContext(\"2d\");");
                jsOutput.AppendLine("ctx" + i + ".fillStyle = 'rgb(\'255,255,255\')';");
                if (h == 0) h = 2;
                jsOutput.AppendLine("ctx" + i + ".rect(" + x + ",(" + (ch - h) + ")," + barWidth + "," + h + ");");
                jsOutput.AppendLine("ctx" + i + ".shadowColor=\"black\";");
                jsOutput.AppendLine("ctx" + i + ".shadowBlur =3;");
                jsOutput.AppendLine("ctx" + i + ".shadowOffsetX=3;");
                jsOutput.AppendLine("ctx" + i + ".shadowOffsetY=-3;");
                jsOutput.AppendLine("ctx" + i + ".fill();");
                jsOutput.AppendLine("ctx.strokeText(\"" + _chartEntities[i].id + "\"," + (x + barWidth / 2) + "," + (canvasHeight - 10) + ");");
                Dictionary<string, int> tc = new Dictionary<string, int>();
                tc.Add("x0", x);
                tc.Add("x1", x + barWidth);
                tc.Add("y", ch - h);
                projectCoordinates[i] = tc;

            }

            clickDetection(projectCoordinates, canvasName);

            return jsOutput.ToString();
        }

        public string generateColor(BurnDown.Models.IChartable itemToColor)
        {
            string colorString;
            double totalHoursEstimated = (double)itemToColor.originalEstimatedHours;
            double hoursWorked = (double)itemToColor.hoursSpentOnItem;
          //  System.Nullable<int> taskDevAvailabilityPerDay = itemToColor.d;


            double percentageWorkCompleted = itemToColor.percentCompleted;
            percentageWorkCompleted = percentageWorkCompleted / 100;
            double percentageOfTimeSpent = hoursWorked / totalHoursEstimated;
            double redValue;
            double greenValue;

            double hoursRemaining = totalHoursEstimated - hoursWorked;



            //compare projected date to (current date + 80% of the time remaining to due date)
            //of it is less keep the color green 
            // (daysSpent/estimation = percentage in terms of time) compare to workCompletedPercentage 
            // 

            string ontimeMin1 = "'rgb(0,28,229)'";
            string ontimeMin2 = "'rgb(0,219,198)'";
            string ontime = "'rgb(13,210,0)'";
            string ontimePlus1 = "'rgb(200,192,0)'";
            string ontimePlus2 = "'rgb(191,0,1)'";

            double status = (percentageOfTimeSpent / percentageWorkCompleted);

            if (status > 0.50)
            {
                colorString = ontimeMin2;
                if (status > 0.90)
                {
                    colorString = ontime;
                    if (status > 1.10)
                    {
                        colorString = ontimePlus1;
                        if (status > 1.50)
                        {
                            colorString = ontimePlus2;
                        }
                    }
                }
            }
            else
                colorString = ontimeMin1;







            return colorString;



        }
    }
}