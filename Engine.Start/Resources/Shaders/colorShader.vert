#version 330 core
layout (location = 0) in vec3 a_Position;

uniform mat4 u_MMatrix;
uniform mat4 u_VMatrix;
uniform mat4 u_PMatrix;
void main()
{
    gl_Position = vec4(a_Position, 1.0) * u_MMatrix * u_VMatrix * u_PMatrix;
}