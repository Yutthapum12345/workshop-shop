﻿
using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks;

public class LoggingBehaviors<TRequest, TResponse>(ILogger<LoggingBehaviors<TRequest,TResponse>> logger)
: IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
where TResponse : notnull
{
   
public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - response={Response} - request data={RequestData}",
         typeof(TRequest).Name, typeof(TResponse).Name, request);
 
        var timer = new Stopwatch();
        timer.Start();
        var response = await next();
 
        timer.Stop();
 
        var timeTaken = timer.Elapsed;
 
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] the request {Request} took {TimeTaken}", typeof(TRequest).Name, timeTaken.Seconds);
        }
 
        logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
 
        return response;
    }
}