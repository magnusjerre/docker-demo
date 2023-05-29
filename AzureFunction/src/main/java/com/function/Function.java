package com.function;

import com.microsoft.azure.functions.ExecutionContext;
import com.microsoft.azure.functions.HttpMethod;
import com.microsoft.azure.functions.HttpRequestMessage;
import com.microsoft.azure.functions.HttpResponseMessage;
import com.microsoft.azure.functions.HttpStatus;
import com.microsoft.azure.functions.annotation.AuthorizationLevel;
import com.microsoft.azure.functions.annotation.FunctionName;
import com.microsoft.azure.functions.annotation.HttpTrigger;

import java.util.Optional;

/**
 * Azure Functions with HTTP Trigger.
 */
public class Function {
    /**
     * This function listens at endpoint "/api/HttpExample". Two ways to invoke it using "curl" command in bash:
     * 1. curl -d "HTTP Body" {your host}/api/HttpExample
     * 2. curl "{your host}/api/HttpExample?name=HTTP%20Query"
     */
    @FunctionName("CleanMessage")
    public HttpResponseMessage run(
            @HttpTrigger(
                name = "req",
                methods = {HttpMethod.GET, HttpMethod.POST},
                authLevel = AuthorizationLevel.ANONYMOUS)
                HttpRequestMessage<Optional<String>> request,
            final ExecutionContext context) {
        context.getLogger().info("Java HTTP trigger processed a request.");

        // Parse query parameter
        var body = request.getBody();

        if (body.isEmpty()) return request.createResponseBuilder(HttpStatus.BAD_REQUEST).body("Must contain a message body").build();

        // Trim spaces, capitalize firt letter, remove trailing dot
        var cleanedMessage = body.get().trim();
        cleanedMessage = cleanedMessage.substring(0, 1).toUpperCase() + cleanedMessage.substring(1);
        if (cleanedMessage.endsWith(".")) {
            cleanedMessage = cleanedMessage.substring(0, cleanedMessage.length() - 1);
        }

        return request.createResponseBuilder(HttpStatus.OK).body(cleanedMessage).build();
    }
}
