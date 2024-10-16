using PrimeNumbers.DTO;
using PrimeNumbers.Interfaces;

namespace PrimeNumbers
{
    internal partial class PrimeNumbersForm : Form
    {
        private ICalculator _calculator;
        private IResultsSaver _resultsSaver;
        private Thread _thread;
        private CancellationTokenSource _cancellationTokenSource;
        private System.Windows.Forms.Timer _timer;
        private bool _isCalculating = false;
        private List<CycleResultDto> _cycleResults = new();

        public PrimeNumbersForm(ICalculator calculator, IResultsSaver resultsSaver)
        {
            InitializeComponent();
            _calculator = calculator;
            _resultsSaver = resultsSaver;
            _cancellationTokenSource = new CancellationTokenSource();
            _calculator.ErrorOccurred += OnErrorOccurred;
            _calculator.CycleCompleted += OnCycleCompleted;
            SetupListView();
            SetupStatusTimer();
        }

        private void UpdateStatusLabel(object sender, EventArgs e)
        {
            if (_isCalculating)
            {
                lblStatus.Text = _calculator.IsCycleRunning ? "Trwa cykl obliczeniowy..." : "Przerwa miêdzy cyklami obliczeniowymi...";
            }
            else
            {
                lblStatus.Text = "Obliczanie nieaktywne";
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            _cycleResults.Clear();

            if (_isCalculating) return;

            listView.Items.Clear();
            _cancellationTokenSource = new CancellationTokenSource();
            _calculator = new Calculator(_cancellationTokenSource.Token);
            _calculator.ErrorOccurred += OnErrorOccurred;
            _calculator.CycleCompleted += OnCycleCompleted;

            _isCalculating = true;

            await _calculator.StartCalculatingAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
            _isCalculating = false;
        }

        private void OnErrorOccurred(string errorMessage)
        {
            _isCalculating = false;
            MessageBox.Show(errorMessage, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnCycleCompleted(CycleResultDto result)
        {
            if (listView.InvokeRequired)
            {
                listView.Invoke(new Action(() => AddCycleResultToListView(result)));
            }
            else
            {
                AddCycleResultToListView(result);
            }

            _cycleResults.Add(result);
        }

        private void AddCycleResultToListView(CycleResultDto result)
        {
            var item = new ListViewItem(result.CycleNumber.ToString());
            item.SubItems.Add(result.LargestPrime.ToString());

            var formattedDuration = $"{result.CycleDuration.Hours:00}:{result.CycleDuration.Minutes:00}:{result.CycleDuration.Seconds:00}.{result.CycleDuration.Milliseconds:000}";
            item.SubItems.Add(formattedDuration);
            item.SubItems.Add(result.ResultTime.ToString("HH:mm:ss"));

            listView.Items.Add(item);
        }

        private async void btnDownloadResults_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                    saveFileDialog.DefaultExt = "xml";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.FileName = "PrimeResults.xml";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var selectedPath = saveFileDialog.FileName;
                        await _resultsSaver.SaveResultsToFileAsync(selectedPath, _cycleResults);
                        MessageBox.Show($"Wyniki zosta³y zapisane w pliku {selectedPath}", "Zapisano wyniki", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"B³¹d podczas zapisywania wyników: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupListView()
        {
            listView.View = View.Details;
            listView.Columns.Add("Numer cyklu", 100);
            listView.Columns.Add("Najwiêksza liczba pierwsza", 200);
            listView.Columns.Add("Czas cyklu", 150);
            listView.Columns.Add("Godzina zakoñczenia cyklu", 200);
            listView.FullRowSelect = true;
            listView.GridLines = true;
        }

        private void SetupStatusTimer()
        {
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += UpdateStatusLabel;
            _timer.Start();
        }
    }
}