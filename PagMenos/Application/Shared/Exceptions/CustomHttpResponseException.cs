using Microsoft.AspNetCore.Mvc;

namespace PagMenos.Application.Shared.Exceptions
{
	public class CustomHttpResponseException :Exception
	{


		public string ErrorCode { get; }
		public int HttpStatusCode { get; }

		public CustomHttpResponseException(string errorCode, string message) : base(message)
		{
			ErrorCode = errorCode;
			HttpStatusCode = GetStatusCodeFromErrorCode(errorCode);
		}

		public CustomHttpResponseException(string errorCode, string message, int httpStatusCode)
			: base(message)
		{
			ErrorCode = errorCode;
			HttpStatusCode = httpStatusCode;
		}

		public CustomHttpResponseException(string errorCode, string message, Exception innerException)
	: base(message, innerException)
		{
			ErrorCode = errorCode;
			HttpStatusCode = GetStatusCodeFromErrorCode(errorCode);
		}

		private int GetStatusCodeFromErrorCode(string errorCode)
		{
			return errorCode switch
			{
				"INVALID_USER_NAME" => 400,

				"COLLABORATOR_UNAUTHORIZED" => 403,
				"COLLABORATOR_NOTFOUND" => 404,
				"INVALID_COLLABORATOR_DATA" => 400,
				"INVALID_ORDER_DATA" => 400,

				"PRODUCT_NOTFOUND" => 404,

				"ORDER_NOTFOUND" => 404,

				"ACCOUNT_NOT_FOUND" => 404,
				"DUPLICATE_ACCOUNT" => 409,
				"JWT_CONFIGURATION_ERROR" => 500,
				"INTERNAL_SERVER_ERROR" => 500,
				"BAD_REQUEST" => 400,
				_ => 500
			};
		}
	}
}
