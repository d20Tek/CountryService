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
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.TelemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
            this._typeName = this.GetType().Name;
        }

        protected ILogger Logger { get; private set; }

        protected TelemetryClient TelemetryClient { get; private set; }

        protected async Task<ActionResult<T>> ControllerOperationAsync<T>(
            string controllerMethod,
            Func<Task<ActionResult<T>>> operation)
        {
            using var controllerOp = this.TelemetryClient.StartOperation<RequestTelemetry>(
                                        $"/{this._typeName}/{controllerMethod}");
            var id = controllerOp.Telemetry.Id;
            var name = controllerOp.Telemetry.Name;

            try
            {
                this.Logger.LogTrace($"[{id}] Begin ControllerOperation '{name}'");
                var result = await operation();
                this.Logger.LogTrace($"[{id}] End ControllerOperation '{name}'");
                return result;
            }
            catch (ArgumentException ex)
            {
                controllerOp.Telemetry.ResponseCode = StatusCodes.Status422UnprocessableEntity.ToString();

                var errorMessage = $"[{id}] Controller '{name}' received invalid input. '{ex.Message}'";
                this.Logger.LogWarning(errorMessage);

                return this.StatusCode(StatusCodes.Status422UnprocessableEntity, errorMessage);
            }
            catch (Exception ex)
            {
                controllerOp.Telemetry.Success = false;
                controllerOp.Telemetry.ResponseCode = StatusCodes.Status500InternalServerError.ToString();

                this.Logger.LogError(ex, $"[{id}] Failed ControllerOperation '{name}' with errror '{ex.Message}'");

                var errorMessage = $"[{id}] An unexpected error occurred on the server.";
                return this.StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }

        protected ActionResult<T> ControllerOperation<T>(
            string controllerMethod,
            Func<ActionResult<T>> operation)
        {
            using var controllerOp = this.TelemetryClient.StartOperation<RequestTelemetry>(
                                                $"/{this._typeName}/{controllerMethod}");
            var id = controllerOp.Telemetry.Id;
            var name = controllerOp.Telemetry.Name;

            try
            {
                this.Logger.LogTrace($"[{id}] Begin ControllerOperation '{name}'");
                var result = operation();
                this.Logger.LogTrace($"[{id}] End ControllerOperation '{name}'");
                return result;
            }
            catch (ArgumentException ex)
            {
                controllerOp.Telemetry.ResponseCode = StatusCodes.Status422UnprocessableEntity.ToString();

                var errorMessage = $"[{id}] Controller '{name}' received invalid input. '{ex.Message}'";
                this.Logger.LogWarning(errorMessage);

                return this.StatusCode(StatusCodes.Status422UnprocessableEntity, errorMessage);
            }
            catch (Exception ex)
            {
                controllerOp.Telemetry.Success = false;
                controllerOp.Telemetry.ResponseCode = StatusCodes.Status500InternalServerError.ToString();

                this.Logger.LogError(ex, $"[{id}] Failed ControllerOperation '{name}' with errror '{ex.Message}'");

                var errorMessage = $"[{id}] An unexpected error occurred on the server.";
                return this.StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
    }
}
