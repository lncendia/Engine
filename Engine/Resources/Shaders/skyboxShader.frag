#version 330 core
out vec4 FragColor;
in vec3 v_Position;

uniform samplerCube texture0;

void main()
{
    FragColor = texture(texture0, v_Position);
}