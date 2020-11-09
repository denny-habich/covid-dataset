using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Covid.Business.Configuration;
using Covid.Business.Dto;
using Covid.Data.Entities;
using Covid.Data.Repositories;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace Covid.Business.Services
{
    public class QuestionaireImportService : ICovidImportService
    {
        private readonly ILogger<QuestionaireImportService> logger;
        private readonly ImportOptions importOptions;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private ImportStatistics import;

        public QuestionaireImportService(ILogger<QuestionaireImportService> logger, IMapper mapper, IOptions<ImportOptions> importOptions, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.importOptions = importOptions.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ImportResponse> Import()
        {
            InitializeImport();
            
            await DoImport();

            AggregateStatistics();

            FinalizeImport();

            return mapper.Map<ImportResponse>(import);
        }

        private async Task DoImport()
        {
            var files = Directory.GetFiles(importOptions.Source, importOptions.FilePattern);
            var csvParserOptions = new CsvParserOptions(false, importOptions.FieldsSeparator);
            var csvMapper = new QuestionaireDataMapping();
            var csvParser = new CsvParser<QuestionaireData>(csvParserOptions, csvMapper);

            var collection = unitOfWork.GetCollection<QuestionaireData>();
            collection.Truncate();
            import.TotalFiles = files.Length;

            foreach (var file in files)
            {
                await ImportFile(csvParser, file, collection);
            }
        }

        private Task ImportFile(CsvParser<QuestionaireData> csvParser, string file, DataCollection<QuestionaireData> collection)
        {
            return Task.Run(() =>
            {
                try
                {
                    var results = csvParser.ReadFromFile(file, Encoding.ASCII)
                        .Select(x => mapper.Map<QuestionaireData>(x.Result))
                        .ToList();

                    collection.InsertMany(results);

                    import.LinesImported += results.Count;
                    import.ImportedFiles += 1;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error importing file {file}");
                }
            });
        }

        private void AggregateStatistics()
        {
            unitOfWork.GetCollection<QuestionaireStatistics>().Truncate();

            AggregateQuestions();

            AggregateCategories();
        }

        private void AggregateCategories()
        {
            var group = new BsonDocument
            {
                {
                    "$group", new BsonDocument
                    {
                        {"_id", "$Category"},
                        {"Count", new BsonDocument {{"$sum", 1}}}
                    }
                }
            };
            var project = new BsonDocument
            {
                {
                    "$project", new BsonDocument
                    {
                        {"_id", 0},
                        {"IntValue", "$_id"},
                        {"Count", 1}
                    }
                }
            };
            var set = new BsonDocument
            {
                {
                    "$set", new BsonDocument
                    {
                        {"Type", StatisticType.Category}
                    }
                }
            };
            var merge = new BsonDocument {{"$merge", nameof(QuestionaireStatistics)}};

            var categoriesPipeline = new[]
            {
                group,
                project,
                set,
                merge
            };

            unitOfWork.GetCollection<QuestionaireData>().Aggregate(categoriesPipeline);
        }

        private void AggregateQuestions()
        {
            var group = new BsonDocument
            {
                {
                    "$group", new BsonDocument
                    {
                        {"_id", "$Question"},
                        {"Count", new BsonDocument {{"$sum", 1}}}
                    }
                }
            };
            var project = new BsonDocument
            {
                {
                    "$project", new BsonDocument
                    {
                        {"_id", 0},
                        {"TextValue", "$_id"},
                        {"Count", 1}
                    }
                }
            };
            var set = new BsonDocument
            {
                {
                    "$set", new BsonDocument
                    {
                        {"Type", StatisticType.Question}
                    }
                }
            };
            var merge = new BsonDocument {{"$merge", nameof(QuestionaireStatistics)}};

            var questionsPipeline = new[]
            {
                group,
                project,
                set,
                merge
            };

            unitOfWork.GetCollection<QuestionaireData>().Aggregate(questionsPipeline);
        }

        private void FinalizeImport()
        {
            import.EndDateTime = DateTime.Now;
            unitOfWork.GetCollection<ImportStatistics>().Update(import.Id, import);
        }

        private void InitializeImport()
        {
            import = new ImportStatistics {StartDateTime = DateTime.Now};
            import = unitOfWork.GetCollection<ImportStatistics>().InsertOne(import);
        }

        private class QuestionaireDataMapping : CsvMapping<QuestionaireData>
        {
            public QuestionaireDataMapping()
            {
                MapProperty(0, x => x.QuestionId);
                MapProperty(1, x => x.Category);
                MapProperty(2, x => x.Question);
                MapProperty(3, x => x.Answer);
                MapProperty(4, x => x.Datetime);
            }
        }
    }
}