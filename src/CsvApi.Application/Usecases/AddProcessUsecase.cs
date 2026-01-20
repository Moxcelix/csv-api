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

    public AddProcessUsecase(
        IProcessRepository processRepository,
        IOperationRepository operationRepository,
        OperationFactory operationFactory,
        ResultFactory resultFactory,
        ProcessFactory processFactory,
        ProcessBindService bindService
    )
    {
        _processRepository = processRepository;
        _operationRepository = operationRepository;
        _operationFactory = operationFactory;
        _resultFactory = resultFactory;
        _processFactory = processFactory;
        _bindService = bindService;
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

        if (process == null)
        {
            process = _processFactory.Create(csv.Name);
            _bindService.Bind(process, operations);
            _processRepository.CreateProcess(process);
            _operationRepository.CreateOperations(operations.ToArray());
        }
        else
        {
            _bindService.Bind(process, operations);
            _operationRepository.DeleteOperationsByProcessId(process.Id);
            _operationRepository.CreateOperations(operations.ToArray());
        }
    }
}
