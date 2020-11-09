using System;
using System.Threading;
using System.Threading.Tasks;
using Covid.Business.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Covid.Business.Services
{
    public class ImportService : IHostedService, IDisposable
    {
        private int _count = 0;
        private Timer _timer;
        private readonly ILogger<ImportService> _logger;
        private readonly IOptions<ImportOptions> _importOptions;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ImportService(IServiceScopeFactory serviceScopeFactory, ILogger<ImportService> logger, IOptions<ImportOptions> importOptions)
        {
            _logger = logger;
            _importOptions = importOptions;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_importOptions.Value.ImportIntervalSeconds < 1)
            {
                _logger.LogInformation("Could not start ImportService because of invalid frequency.");
                return Task.CompletedTask;
            }

            _logger.LogInformation("ImportService starting");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_importOptions.Value.ImportIntervalSeconds));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _count++;

            _logger.LogInformation("ImportService starts doing work. Count: {Count}", _count);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var importer = scope.ServiceProvider.GetRequiredService<ICovidImportService>();
                importer.Import();
            }

            _logger.LogInformation("ImportService finished doing work. Count: {Count}", _count);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ImportService stopping");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
    
}
