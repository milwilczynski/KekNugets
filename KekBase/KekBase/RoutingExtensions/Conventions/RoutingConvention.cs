using System.Text;
using KekBase.BaseConfigurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace KekBase.RoutingExtensions.Conventions;

public class RoutingConvention : IControllerModelConvention
{
    private readonly string _apiVersion;
    private readonly string _domain;

    public RoutingConvention(AppConfiguration configuration)
    {
        _domain = configuration.Domain;
        _apiVersion = configuration.ApiVersion;
    }

    public void Apply(ControllerModel controller)
    {
        //without skipping RouteAnalyzer swagger dosen't work, sadge
        if (controller.ControllerName == "RouteAnalyzer_Main")
        {
            return;
        }

        var hasRouteAttributes = controller.Selectors.Any(selector =>
            selector.AttributeRouteModel != null);
        if (hasRouteAttributes)
        {
            return;
        }

        var namespc = controller.ControllerType.Namespace;
        if (namespc == null)
        {
            return;
        }

        var template = new StringBuilder();
        template.Append($"/api/v{_apiVersion}/{_domain}");
        template.Append("/[controller]/[action]");

        foreach (var selector in controller.Selectors.ToList())
        {
            selector.AttributeRouteModel =
                AttributeRouteModel.CombineAttributeRouteModel(
                    new AttributeRouteModel(new RouteAttribute(template.ToString())),
                    selector.AttributeRouteModel);
        }
    }
}