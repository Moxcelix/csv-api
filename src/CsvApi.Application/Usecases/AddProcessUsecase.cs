using CsvApi.Domain;

namespace CsvApi.Application;

public class AddProcessUsecase
{
    private readonly IProcessRepository _processRepository;
    private readonly IOperationRepository _operationRepository;
    private readonly OperationFactory _operationFactory;
    private readonly ProcessFactory _processFactory;
    private readonly ProcessBindService _bindService;
    private readonly ResultFactory _resultFactory;
    private readonly IResultRepository _resultRepository;
    private readonly ResultCalculateService _resultCalculateService;

    public AddProcessUsecase(
        IProcessRepository processRepository,
        IOperationRepository operationRepository,
        OperationFactory operationFactory,
        ResultFactory resultFactory,
        IResultRepository resultRepository,
        ProcessFactory processFactory,
        ProcessBindService bindService,
        ResultCalculateService resultCalculateService
    )
    {
        _processRepository = processRepository;
        _operationRepository = operationRepository;
        _operationFactory = operationFactory;
        _resultFactory = resultFactory;
        _resultRepository = resultRepository;
        _processFactory = processFactory;
        _bindService = bindService;
        _resultCalculateService = resultCalculateService;
    }

    public void Execute(CsvDTO csv)
    {
        var operations = new List<Operation>();

        foreach (var record in csv.Records)
        {
            var operation = _operationFactory.Create(
                record.StartDate,
                record.ExecutionTimeSeconds,
                record.Value
            );

            operations.Add(operation);
        }

        var process = _processRepository.GetProcessByName(csv.Name);
        var operationArray = operations.ToArray();

        if (process == null)
        {
            process = _processFactory.Create(csv.Name);
            _bindService.Bind(process, operationArray);
            var result = _resultFactory.Create(process);
            _resultCalculateService.CalculateResult(result, operationArray);
            _processRepository.CreateProcess(process);
            _operationRepository.CreateOperations(operationArray);
            _resultRepository.CreateResult(result);
        }
        else
        {
            _bindService.Bind(process, operationArray);
            var result = _resultRepository.GetResultByProcessId(process.Id);
            _resultCalculateService.CalculateResult(result, operationArray);
            _operationRepository.DeleteOperationsByProcessId(process.Id);
            _operationRepository.CreateOperations(operationArray);
            _resultRepository.UpdateResult(result);
        }
    }
}
