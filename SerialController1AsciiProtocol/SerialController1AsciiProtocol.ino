#define SENSOR1 23 // 23 controller 1, 0 controller 2
#define SENSOR2 17 // 17 controller 1, 0 controller 2
#define SENSOR3 16 // 16 controller 1, 0 controller 2
#define ACTUATOR1 32 // 32 controller, 23 controller 2
#define ACTUATOR2 25 // 25 controller, 23 controller 2
#define ACTUATOR3 26 // 26 controller, 23 controller 2

void setup()
{
  Serial.begin(115200);
  pinMode(SENSOR1, INPUT_PULLUP);
  pinMode(SENSOR2, INPUT_PULLUP);
  pinMode(SENSOR3, INPUT_PULLUP);
  pinMode(ACTUATOR1, OUTPUT);
  pinMode(ACTUATOR2, OUTPUT);
  pinMode(ACTUATOR3, OUTPUT);
}

void loop()
{

  if (Serial.available() > 0) {

    String dataRx = Serial.readStringUntil('\n');
    if (dataRx == "sensores") {
      Serial.print('1');
      Serial.print(',');
      Serial.print(digitalRead(SENSOR1));
      Serial.print(',');
      Serial.print('2');
      Serial.print(',');
      Serial.print(digitalRead(SENSOR2));
      Serial.print(',');
      Serial.print('3');
      Serial.print(',');
      Serial.print(digitalRead(SENSOR3));
      Serial.print('\n');
    }
    else if (dataRx == "actuadores") {
      Serial.print('1');
      Serial.print(',');
      Serial.print(digitalRead(ACTUATOR1));
      Serial.print(',');
      Serial.print('2');
      Serial.print(',');
      Serial.print(digitalRead(ACTUATOR2));
      Serial.print(',');
      Serial.print('3');
      Serial.print(',');
      Serial.print(digitalRead(ACTUATOR3));
      Serial.print('\n');
    }
    else if (dataRx == "1,0") {
      digitalWrite(ACTUATOR1, 0);
    }
    else if (dataRx == "1,1") {
      digitalWrite(ACTUATOR1, 1);
    }
    else if (dataRx == "2,0") {
      digitalWrite(ACTUATOR2, 0);
    }
    else if (dataRx == "2,1") {
      digitalWrite(ACTUATOR2, 1);
    }
    else if (dataRx == "3,0") {
      digitalWrite(ACTUATOR3, 0);
    }
    else if (dataRx == "3,1") {
      digitalWrite(ACTUATOR3, 1);
    }
  }
}
