using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreDBPackage.CCC {
    public class ActionHandler : ActionFilterAttribute {
        public string IdParamName { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context) {
            //do your staff here
        }
        //1 before
        public override void OnActionExecuting(ActionExecutingContext context) {
            //şimdilik servis bazlı yapıyorum
            //switch (serviceEnum) {
            //    case ServiceEnum.register:
            //        var model = context.ActionArguments["model"] as LoginRequestModel;
            //        //if(string.IsNullOrWhiteSpace(model.email) || string.IsNullOrWhiteSpace(model.password))
            //        //return ü hallet
            //        break;
            //    default:
            //        break;
            //}
        }
        public override void OnResultExecuted(ResultExecutedContext context) {
            //do your staff here
        }
        public override void OnResultExecuting(ResultExecutingContext context) {
            //do your staff here
        }
    }
}
