#version 410 core

in vec3 normal;
in vec4 fragPosEye;
in vec4 fragPosLightSpace;
in vec2 fragTexCoords;
in vec4 pointLightEye;
vec3 pointLightColor=vec3(0.0f,0.0f,1.0f);
vec3 ambientPoint;
vec3 diffusePoint;
vec3 specularPoint;

out vec4 fColor;

//lighting
uniform	mat3 normalMatrix;
uniform mat3 lightDirMatrix;
uniform	vec3 lightColor;
uniform	vec3 lightDir;
uniform sampler2D diffuseTexture;
uniform sampler2D specularTexture;
uniform sampler2D shadowMap;


vec3 ambient;
float ambientStrength = 0.2f;
vec3 diffuse;
vec3 specular;
float specularStrength = 0.5f;
float shininess = 64.0f;
uniform float fogDensity; 
vec3 o;

void computeLightComponents()
{		
	vec3 cameraPosEye = vec3(0.0f);//in eye coordinates, the viewer is situated at the origin
	
	//transform normal
	vec3 normalEye = normalize(normalMatrix * normal);	
	
	//compute light direction
	vec3 lightDirN = normalize(lightDirMatrix * lightDir);	

	//compute view direction 
	vec3 viewDirN = normalize(cameraPosEye - fragPosEye.xyz);
	
	//compute half vector
	vec3 halfVector = normalize(lightDirN + viewDirN);
		
	//compute ambient light
	ambient = ambientStrength * lightColor;
	
	//compute diffuse light
	diffuse = max(dot(normalEye, lightDirN), 0.0f) * lightColor;
	
	//compute specular light
	float specCoeff = pow(max(dot(halfVector, normalEye), 0.0f), shininess);
	specular = specularStrength * specCoeff * lightColor;
}

void computePointLightComponents()
{		
	float constant = 1.0f;
	float linear = 0.004f;
	float quadratic = 0.00005f;
	
	vec3 cameraPosEye = vec3(0.0f);//in eye coordinates, the viewer is situated at the origin
	
	//transform normal
	vec3 normalEye = normalize(normalMatrix * normal);	
	
	//compute light direction
	vec3 lightDirN = normalize(pointLightEye.xyz - fragPosEye.xyz);	

	//compute view direction 
	vec3 viewDirN = normalize(cameraPosEye - fragPosEye.xyz);
	
	float dist= length(pointLightEye.xyz - fragPosEye.xyz);
	float att = 1.0f/(constant+linear*dist+quadratic*dist*dist);

	//compute half vector
	vec3 halfVector = normalize(lightDirN + viewDirN);
	
	ambientPoint = att*(ambientStrength+0.1f)*pointLightColor;
	//compute diffuse light
	diffusePoint = att*max(dot(normalEye, lightDirN), 0.0f) * lightColor;
	
	//compute specular light
	float specCoeff = pow(max(dot(halfVector, normalEye), 0.0f), shininess);
	specularPoint = att*specularStrength * specCoeff * lightColor;
}

float computeShadow()
{	
	// perform perspective divide
    vec3 normalizedCoords = fragPosLightSpace.xyz / fragPosLightSpace.w;
    if(normalizedCoords.z > 1.0f)
        return 0.0f;
    // Transform to [0,1] range
    normalizedCoords = normalizedCoords * 0.5f + 0.5f;
    // Get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
    float closestDepth = texture(shadowMap, normalizedCoords.xy).r;    
    // Get depth of current fragment from light's perspective
    float currentDepth = normalizedCoords.z;
    // Check whether current frag pos is in shadow
    float bias = 0.005f;
    float shadow = currentDepth - bias> closestDepth  ? 1.0f : 0.0f;

    return shadow;	
}

float computeFog()
{
 
 float fragmentDistance = length(fragPosEye);
 float fogFactor = exp(-pow(fragmentDistance * fogDensity, 2));

 return clamp(fogFactor, 0.0f, 1.0f);
}

void main() 
{
	computeLightComponents();
	computePointLightComponents();
	float shadow = computeShadow();
	
	
	vec3 ambientComp = ambient + ambientPoint;
	vec3 diffuseComp = ambientComp + (1.0f - shadow)*diffuse + diffusePoint;
	vec3 specularComp = (1.0f - shadow) * specular + specularPoint;
	
	vec3 color = min(diffuseComp * vec3(texture(diffuseTexture, fragTexCoords)) + specularComp * vec3(texture(specularTexture, fragTexCoords)), 1.0f);

    fColor = vec4(color, 1.0f);
	float fogFactor = computeFog();
	vec4 fogColor = vec4(0.5f, 0.5f, 0.5f, 1.0f);
	fColor = mix(fogColor, fColor, fogFactor);
	fColor = fogColor * (1 - fogFactor) + fColor * fogFactor;
}
