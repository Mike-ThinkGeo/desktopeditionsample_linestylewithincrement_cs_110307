using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;


namespace  LineStyleWithIncrement
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.CurrentExtent = new RectangleShape(-10778878,3909451,-10775821,3907529);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            //Displays the World Map Kit as a background.
            ThinkGeo.MapSuite.DesktopEdition.WorldMapKitWmsDesktopOverlay worldMapKitDesktopOverlay = new ThinkGeo.MapSuite.DesktopEdition.WorldMapKitWmsDesktopOverlay();
            worldMapKitDesktopOverlay.Projection = WorldMapKitProjection.SphericalMercator;
            winformsMap1.Overlays.Add(worldMapKitDesktopOverlay);

            //We set the lineStyle for TrackShapeLayer as the Increment Line Style.
            CustomIncrementLineStyle incrementLineStyle = new CustomIncrementLineStyle();
            winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = incrementLineStyle;
            winformsMap1.TrackOverlay.TrackShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            winformsMap1.TrackOverlay.TrackMode = TrackMode.Line;

            winformsMap1.Refresh();
        }

      
        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            //Displays world coordinates.
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(pointShape.X, 4) + " Y:" + Math.Round(pointShape.Y, 4);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
