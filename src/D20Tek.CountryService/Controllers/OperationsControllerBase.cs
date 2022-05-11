//---------------------------------------------------------------------------------------------------------------------
// Copyright (c) d20Tek.  All rights reserved.
//---------------------------------------------------------------------------------------------------------------------
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc;

namespace D20Tek.CountryService.Controllers
{
    public abstract class OperationsControllerBase : ControllerBase
    {
        private readonly string _typeName;

        public OperationsControllerBase(ILogger logger, TelemetryClient telemetryClient)
        {
            this.Logger = logger;
            this.TelemetryClient = telemetryClient;
            this._typeName = this.GetType().Name;
        }

        protected ILogger Logger { get; private set; }

        protected TelemetryClient TelemetryClient { get; private set; }

        protected async Task<ActionResult<T>> ControllerOperationAsync<T>(string controllerMethod, Func<Task<ActionResult<T>>> operation)
        {
            using var controllerOperation = this.TelemetryClient.StartOperation<RequestTelemetry>($"/{this._typeName}/{controllerMethod}");

            try
            {
                this.Logger.LogTrace($"[{controllerOperation.Telemetry.Id}] Begin ControllerOperation '{controllerOperation.Telemetry.Name}'");
                var result = await operation();
                this.Logger.LogTrace($"[{controllerOperation.Telemetry.Id}] End ControllerOperation '{controllerOperation.Telemetry.Name}'");
                return result;
            }
            catch (ArgumentException ex)
            {
                controllerOperation.Telemetry.ResponseCode = StatusCodes.Status422UnprocessableEntity.ToString();

                var controllerErrorMessage = $"[{controllerOperation.Telemetry.Id}] Controller '{controllerOperation.Telemetry.Name}' received invalid input. '{ex.Message}'";
                this.Logger.LogWarning(controllerErrorMessage);

                return this.StatusCode(StatusCodes.Status422UnprocessableEntity, controllerErrorMessage);
            }
            catch (Exception ex)
            {
                controllerOperation.Telemetry.Success = false;
                controllerOperation.Telemetry.ResponseCode = StatusCodes.Status500InternalServerError.ToString();

                this.Logger.LogError(ex, $"[{controllerOperation.Telemetry.Id}] Failed ControllerOperation '{controllerOperation.Telemetry.Name}' with errror '{ex.Message}'");

                var controllerErrorMessage = $"[{controllerOperation.Telemetry.Id}] An unexpected error occurred on the server.";
                return this.StatusCode(StatusCodes.Status500InternalServerError, controllerErrorMessage);
            }
        }

        protected ActionResult<T> ControllerOperation<T>(string controllerMethod, Func<ActionResult<T>> operation)
        {
            using var controllerOperation = this.TelemetryClient.StartOperation<RequestTelemetry>($"/{this._typeName}/{controllerMethod}");

            try
            {
                this.Logger.LogTrace($"[{controllerOperation.Telemetry.Id}] Begin ControllerOperation '{controllerOperation.Telemetry.Name}'");
                var result = operation();
                this.Logger.LogTrace($"[{controllerOperation.Telemetry.Id}] End ControllerOperation '{controllerOperation.Telemetry.Name}'");
                return result;
            }
            catch (ArgumentException ex)
            {
                controllerOperation.Telemetry.ResponseCode = StatusCodes.Status422UnprocessableEntity.ToString();

                var controllerErrorMessage = $"[{controllerOperation.Telemetry.Id}] Controller '{controllerOperation.Telemetry.Name}' received invalid input. '{ex.Message}'";
                this.Logger.LogWarning(controllerErrorMessage);

                return this.StatusCode(StatusCodes.Status422UnprocessableEntity, controllerErrorMessage);
            }
            catch (Exception ex)
            {
                controllerOperation.Telemetry.Success = false;
                controllerOperation.Telemetry.ResponseCode = StatusCodes.Status500InternalServerError.ToString();

                this.Logger.LogError(ex, $"[{controllerOperation.Telemetry.Id}] Failed ControllerOperation '{controllerOperation.Telemetry.Name}' with errror '{ex.Message}'");

                var controllerErrorMessage = $"[{controllerOperation.Telemetry.Id}] An unexpected error occurred on the server.";
                return this.StatusCode(StatusCodes.Status500InternalServerError, controllerErrorMessage);
            }
        }
    }
}
