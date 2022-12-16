using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable, PostProcess(typeof(OutlinePostProcess), PostProcessEvent.BeforeStack, "Custom/Outline")]
public sealed class Outline : PostProcessEffectSettings
{
    [Range(1f, 5f), Tooltip("Outline thickness.")]
    public IntParameter thickness = new IntParameter { value = 2 };


    [Range(0f, 5f), Tooltip("Outline edge start.")]
    public FloatParameter edge = new FloatParameter { value = 0.1f };


    [Range(0f, 1f), Tooltip("Outline smoothness transition on close objects.")]
    public FloatParameter transitionSmoothness = new FloatParameter { value = 0.2f };


    [Tooltip("Outline color.")]
    public ColorParameter color = new ColorParameter { value = Color.black };
}
public sealed class OutlinePostProcess : PostProcessEffectRenderer<Outline>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Outline"));
        sheet.properties.SetInt("_Thickness", settings.thickness);
        sheet.properties.SetFloat("_TransitionSmoothness", settings.transitionSmoothness);
        sheet.properties.SetFloat("_Edge", settings.edge);
        sheet.properties.SetColor("_Color", settings.color);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}