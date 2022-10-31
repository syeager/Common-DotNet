using System.Net;
using LittleByte.Common.AspNet.Responses;
using NUnit.Framework;

namespace LittleByte.Test.AspNet;

public static class ApiAssert
{
    public static void IsSuccess<T>(ApiResponse<T> response, HttpStatusCode expectedCode = HttpStatusCode.OK)
        where T : class
    {
        Assert.IsFalse(response.IsError);
        Assert.IsNotNull(response.Obj);
        Assert.AreEqual((int)expectedCode, response.StatusCode);
    }

    public static void IsFailure(ApiResponse response, HttpStatusCode expectedCode = HttpStatusCode.BadRequest)
    {
        Assert.IsTrue(response.IsError);
        Assert.AreEqual((int)expectedCode, response.StatusCode);
    }
}