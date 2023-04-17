#version 330 core
out vec4 FragColor;
in vec3 v_Normal;
in vec3 v_Position;
in vec2 v_Texture;

uniform vec3 u_LightDirection;
uniform vec3 u_CamPosition;
uniform vec3 u_AmbientLightColor;
uniform vec3 u_DiffuseLightColor;
uniform vec3 u_SpecularLightColor;
uniform float u_M;
uniform sampler2D texture0;

void main()
{
    vec3 norm = normalize(v_Normal);
    float diff = max(dot(norm, -u_LightDirection), 0.0);
    vec3 reflectionVector = normalize(reflect(u_LightDirection, norm));
    float specularLightDot = max(dot(reflectionVector, normalize(u_CamPosition - v_Position)), 0.0);
    float specularLightParam = pow(specularLightDot, u_M);
    FragColor = texture(texture0, v_Texture) * vec4(u_AmbientLightColor + u_DiffuseLightColor * diff + u_SpecularLightColor * specularLightParam, 1);
}