using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IDAProject.Web.Api.Models.Interfaces.Managers;
using IDAProject.Web.Api.Models.Interfaces.Repositories;
using IDAProject.Web.LabelPrintingService;
using IDAProject.Web.LabelPrintingService.Models;
using IDAProject.Web.Models.Dto.OrderLines;
using IDAProject.Web.Models.Dto.Printers;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.RequestModels.Printers;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace IDAProject.Web.Api.Managers
{
    public class PrintersManager : IPrintersManager
    {
        private readonly IPrintersRepository _printersRepository;
        private readonly IPrinterService _printerService;
        private readonly IUsersManager _usersManager;
        private readonly ILogger _logger;

        public PrintersManager(ILogger<PrintersManager> logger, IPrintersRepository PrintersRepository, IUsersManager usersManager, IPrinterService printerService)
        {
            _logger = logger;
            _printersRepository = PrintersRepository;
            _usersManager = usersManager;
            _printerService = printerService;
        }
        public async Task<ResponseModelList<PrinterDto>> SearchPrintersAsync(SearchPrintersParams searchParams)
        {
            var result = new ResponseModelList<PrinterDto>();
            try
            {
                result.Payload = await _printersRepository.SearchPrintersAsync(searchParams);
                result.Valid = true;
            }   
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(searchParams);
                _logger.LogError(e,$"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModel<PrinterDto>> GetPrinterByIdAsync(int id)
        {
            var result = new ResponseModel<PrinterDto>();
            try
            {
                result.Payload = await _printersRepository.GetPrinterByIdAsync(id);
                if (result.Payload == null)
                {
                    result.Message = "The Printer  with the specified id could not be found.";
                }
                else
                {
                    result.Valid = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModelBase> DeletePrinterAsync(int id, int? userId)
        {
            var result = new ResponseModelBase();
            try
            {
                await _printersRepository.DeletePrinterAsync(id, userId);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                _logger.LogError(e, $"id: {id}");
            }
            return result;
        }

        public async Task<ResponseModel<int>> SavePrinterAsync(SavePrinterRequestModel requestModel)
        {
            var result = new ResponseModel<int>();
            try
            {
                result.Payload = await _printersRepository.SavePrinterAsync(requestModel);
                result.Valid = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                var reqModel = JsonConvert.SerializeObject(requestModel);
                _logger.LogError(e, $"request model: {reqModel}");
            }
            return result;
        }

        public async Task<ResponseModelBase> QueuePrintAsync(OrderLineDto line,int userId)
        {
            if (_printerService == null)
            {
                throw new InvalidOperationException("PrinterService is not initialized.");
            }

            var user = await _usersManager.GetUserByIdAsync(userId);


            var label = new WarehouseLabel() 
            {
                ItemNumber = line.FebiArticleNo,
                OrderNumber = line.CustomerOrderNumber,
                PositionInOrder = $@"{line.CheckedQuantity}/{line.RequestedQuantity}",
                TourName = line.TourName
            };
            var printerInfo = await _printersRepository.GetPrinterByIdAsync(user.Payload.PrinterId.Value);
            return await _printerService.QueuePrintAsync(new PrintJob()
            {
                PrinterId = user.Payload.PrinterId.Value,
                ZplData = label.CompleteZpl

            }, printerInfo);
        }

        public async Task<ResponseModelBase> SendCustomCommandWithResponse(CustomPrint command)
        {
            var internalCommand = new CustomPrintCommand()
            {
                CommandData = command.CommandData,
                IpAddress = command.IpAddress,
                Port = command.Port
            };
            return await _printerService.SendCustomCommandsAsync(internalCommand);

        }

        public async Task<ResponseModelBase> CheckPrinterStatusAsync(CustomPrint command)
        {
            return await _printerService.CheckPrinterStatusAsync(command.IpAddress, command.Port);
        }
    }
}
