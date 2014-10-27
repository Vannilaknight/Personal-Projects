using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BoydT_ConwayLife.TaylorControls;
using BoydT_ConwayLife.Models;
using System.Timers;
using BoydT_ConwayLife.TypeConverterts;

namespace BoydT_ConwayLife {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        static Timer timer;
        bool playPause = true;
        Cell[,] cells;
        Cell[,] nextGenCells;
        int myColumn, myRow;
        int columnNum = 10;
        int rowNum = 10;

        //Initializes the program
        public MainWindow() {
            InitializeComponent();
            createGrid(new Size(10, 10));
            mainGrid.DataContext = widthSlider;

        }

        //changes grid when width slider is changed
        private void widthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            columnNum = (int)widthSlider.Value;
            createGrid(new Size(columnNum, rowNum));
        }

        //Changes grid when height slider is changed
        private void heightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            rowNum = (int)heightSlider.Value;
            createGrid(new Size(columnNum, rowNum));
        }

        //Updates cell based on neighbor conditions
        public void updateCells() {
            for (int n = 0; n < myColumn; n++) {
                for (int m = 0; m < myRow; m++) {
                    nextGenCells[n, m] = new Cell();
                    bool living = cells[n, m].isAlive;
                    int count = GetLivingNeighbors(n, m);
                    bool result = false;
                    if (living && count < 2)
                        result = false;
                    if (living && (count == 2 || count == 3))
                        result = true;
                    if (living && count > 3)
                        result = false;
                    if (!living && count == 3)
                        result = true;


                    nextGenCells[n, m].isAlive = result;
                }
            }
            updateNextGenCells();
        }

        //sets the current cells living conditions
        public void updateNextGenCells() {
            for (int f = 0; f < myColumn; f++) {
                for (int k = 0; k < myRow; k++) {
                    cells[f, k].isAlive = nextGenCells[f, k].isAlive;
                }
            }
        }

        //creates grid using width and height
        private void createGrid(Size recSize) {
            if (cellGrid != null) {
                cellGrid.Children.Clear();
                myColumn = (int)(recSize.Width);
                myRow = (int)(recSize.Height);
                cellGrid.Columns = (int)(recSize.Width);
                cellGrid.Rows = (int)(recSize.Height);
                cells = new Cell[(int)recSize.Width, (int)recSize.Height];
                nextGenCells = new Cell[(int)recSize.Width, (int)recSize.Height];
                for (int j = 0; j < cellGrid.Rows; j++) {
                    for (int i = 0; i < cellGrid.Columns; i++) {

                        cells[i, j] = new Cell();

                        Rectangle block = new Rectangle();
                        block.DataContext = cells[i, j];
                        block.MouseLeftButtonDown += cells[i, j].ParentClicked;
                        block.Margin = new Thickness(1);

                        Binding b = new Binding("isAlive");
                        b.Source = cells[i, j];
                        b.Converter = (BoolColorConverter)Application.Current.FindResource("cellLifeSwitch");
                        block.SetBinding(Rectangle.FillProperty, b);

                        cellGrid.Children.Add(block);
                    }

                }
            }
        }

        //gets the amount of neighbors alive that are adjacent to coords entered
        public int GetLivingNeighbors(int x, int y) {
            int count = 0;

            // Check cell on the right.
            if (x != myColumn - 1)
                if (cells[x + 1, y].isAlive)
                    count++;

            // Check cell on the bottom right.
            if (x != myColumn - 1 && y != myRow - 1)
                if (cells[x + 1, y + 1].isAlive)
                    count++;

            // Check cell on the bottom.
            if (y != myRow - 1)
                if (cells[x, y + 1].isAlive)
                    count++;

            // Check cell on the bottom left.
            if (x != 0 && y != myRow - 1)
                if (cells[x - 1, y + 1].isAlive)
                    count++;

            // Check cell on the left.
            if (x != 0)
                if (cells[x - 1, y].isAlive)
                    count++;

            // Check cell on the top left.
            if (x != 0 && y != 0)
                if (cells[x - 1, y - 1].isAlive)
                    count++;

            // Check cell on the top.
            if (y != 0)
                if (cells[x, y - 1].isAlive)
                    count++;

            // Check cell on the top right.
            if (x != myColumn - 1 && y != 0)
                if (cells[x + 1, y - 1].isAlive)
                    count++;
            return count;
        }

        //Button to start next generation
        private void nextGen(object sender, RoutedEventArgs e) {
            updateCells();
        }

        //starts the program
        private void Play(object sender, RoutedEventArgs e) {
            if (playPause) {
                playPause = !playPause;
                timer = new Timer((1000 / genSlider.Value));
                timer.Elapsed += timer_Elapsed;
                timer.Start();
                startButton.Content = "Stop";
            } else {
                playPause = !playPause;
                timer.Stop();
                startButton.Content = "Play";
            }

        }

        //Calls this method for ever interval of the timer
        void timer_Elapsed(object sender, ElapsedEventArgs e) {
            updateCells();
        }

        //Clears the board
        private void Reset(object sender, RoutedEventArgs e) {
            cellGrid.Children.Clear();
            createGrid(new Size(widthSlider.Value, heightSlider.Value));
        }

        //Fills cell grid with random cells
        private void Randomize(object sender, RoutedEventArgs e) {
            Random rand = new Random();

            foreach (Cell cell in cells) {
                cell.isAlive = false;
            }

            foreach (Cell cell in cells) {

                if (rand.Next(1, 3) == 1) {
                    cell.isAlive = true;
                }

            }
        }
    }
}
