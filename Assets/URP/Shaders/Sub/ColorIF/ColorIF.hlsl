#ifndef SHL_INCLUDED
#define SHL_INCLUDED

void ColorF_float
(
	float Value,
	half4 C1,
	half4 C2,
	half4 C3,
	half4 C4,
	half4 C5,
	out half4 OutColor
)
{
	switch (Value)
	{
		case 0:
			OutColor = C1;
			break;
		case 1:
			OutColor = C2;
			break;
		case 2:
			OutColor = C3;
			break;
		case 3:
			OutColor = C4;
			break;
		case 4:
			OutColor = C5;
			break;
	}
}
#endif