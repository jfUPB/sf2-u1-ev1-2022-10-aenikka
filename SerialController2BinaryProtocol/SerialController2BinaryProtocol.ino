#define SENSOR 33
#define ACTUATOR 25

void setup()
{
  Serial.begin(115200);
  pinMode(SENSOR, INPUT_PULLUP);
  pinMode(ACTUATOR, OUTPUT);
}

void loop()
{
  // IMPLEMENTA EL PROTOCOLO
}
