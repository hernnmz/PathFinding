namespace GUI
{
    using System;
    using System.Windows.Forms;
    using Algorithms;
    using Grid;
    using System.Linq;
    using System.Collections.Generic;

    public partial class Main : Form
    {
        private OpenFileDialog _openFileDialog;
        private string string_open;
        private LongestRoute _longestRoute;
        private int _pagina;
        private int _cantidadRegistros = 1000;
        string[] _lineasArchivo;
        string[] _tamanoMatriz;

        public Main()
        {
            InitializeComponent();
            this.btnGo.Enabled = false;
        }

        /// <summary>
        /// Set up a maze and initialise the algorithm variables
        /// </summary>
        private void InitializeData(Matrix dataMatrix)
        {
            Text = @"Path Finding... ";
            _longestRoute = new LongestRoute(dataMatrix);
        }

        /// <summary>
        /// Draw the found path onto the maze
        /// </summary>
        /// <param name="details"></param>
        private void BuildPath(SearchDetails details)
        {
            this.textBoxNodes.AppendText("Page ");
            this.textBoxNodes.AppendText(_pagina.ToString());
            this.textBoxNodes.AppendText("\r\n");

            Matrix matrix = _longestRoute.GetDataMatrix();
            List<Node> listClosed = _longestRoute.GetListClosed();
            List<Coord> listPath = new List<Coord>();

            listPath.AddRange(details.Path);
            int? parentId = details.CurrentNodeId;

            while (parentId.HasValue)
            {
                var nextNode = listClosed.First(x => x.Id == parentId);
                listPath.Add(nextNode.Coord);
                parentId = nextNode.ParentId;
            }

            listPath.Reverse();

            this.textBoxNodes.AppendText("Length Path ");
            this.textBoxNodes.AppendText(listPath.Count.ToString());
            this.textBoxNodes.AppendText("\r\n");

            this.textBoxNodes.AppendText("Drop ");
            int drop = matrix.Data[listPath.First().X, listPath.First().Y] - matrix.Data[listPath.Last().X, listPath.Last().Y];
            this.textBoxNodes.AppendText(drop.ToString());
            this.textBoxNodes.AppendText("\r\n");

            this.textBoxNodes.AppendText("Path => ");

            for (var i = 0; i < listPath.Count; i++)
            {
                var step = listPath[i];
                if (step.X >= 0 && step.Y >= 0)
                {
                    this.textBoxNodes.AppendText(matrix.Data[step.X, step.Y].ToString());
                    this.textBoxNodes.AppendText("[");
                    this.textBoxNodes.AppendText(step.X.ToString());
                    this.textBoxNodes.AppendText(",");
                    this.textBoxNodes.AppendText(step.Y.ToString());
                    this.textBoxNodes.AppendText("]");
                }

                if (i >= 0 && i < listPath.Count - 1)
                {
                    this.textBoxNodes.AppendText("->");
                }
            }
            this.textBoxNodes.AppendText("\r\n");
        }

        /// <summary>
        /// Set the next search algorithm running
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGo_Click(object sender, EventArgs e)
        {
            this.ButtonFileUpload.Enabled = true;
            if(_pagina < _cantidadRegistros)
                UploadMatrix();
                RecursiveProcess();
        }

        private void RecursiveProcess()
        {
            SearchDetails searchStatus = _longestRoute.GetPathTick();

            // If the path is found, draw the path, otherwise draw the updated search
            if (searchStatus.PathFound)
            {
                if (searchStatus.CountSortedListNode <= 0)
                {
                    BuildPath(searchStatus);
                }
                else if (searchStatus.CountSortedListNode > 0)
                {
                    searchStatus.PathFound = false;
                    _longestRoute.InitializeOrigin();
                    _longestRoute.InitializeOpenList();
                }
            }
            if (!searchStatus.PathFound)
                RecursiveProcess();
        }

        private void ButtonFileUpload_Click(object sender, EventArgs e)
        {
            UploadFileData();
            this.ButtonFileUpload.Enabled = false;
            this.btnGo.Enabled = true;
        }

        private void UploadFileData()
        {
            _openFileDialog = new OpenFileDialog
            {
                Filter = "Text file (*.txt)|*.txt|All file(*.*)|*.*"
            };

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string_open = _openFileDialog.FileName;
                _lineasArchivo = System.IO.File.ReadAllText(string_open).Replace("\r\n", "*").Split('*');
                _tamanoMatriz = _lineasArchivo[0].Split(' ');
            }
        }

        private void UploadMatrix()
        {
            Matrix dataMatrix = new Matrix();
            List<Cell> listNode;

            dataMatrix.Rows = int.Parse(_tamanoMatriz[0]);
            dataMatrix.Columns = int.Parse(_tamanoMatriz[1]);

            dataMatrix.Data = new int[dataMatrix.Rows, dataMatrix.Columns];
            listNode = new List<Cell>();

            // Maximum 33 * 33 to resolve all paths
            // 1000 * 1000 only process top 1547 nodes
            for (int contadorY = 0; contadorY <= dataMatrix.Rows - 1; contadorY++)
            {
                string[] linea = _lineasArchivo[contadorY + 1].Split(' ');
                for (int contadorX = 0; contadorX <= dataMatrix.Columns - 1; contadorX++)
                {
                    dataMatrix.Data[contadorX, contadorY] = int.Parse(linea[contadorX]);
                    Cell cell = new Cell
                    {
                        Coord = new Coord(contadorX, contadorY),
                        Weight = int.Parse(linea[contadorX])
                    };

                    listNode.Add(cell);
                }
            }
            // 1547
            _pagina = _pagina + 1;
            if(dataMatrix.Columns == _cantidadRegistros)
                dataMatrix.SortedListNode.AddRange(listNode.Skip(_pagina * _cantidadRegistros).Take(_cantidadRegistros));
            else
                dataMatrix.SortedListNode.AddRange(listNode.Take(_cantidadRegistros));
            InitializeData(dataMatrix);
        }
    }
}
