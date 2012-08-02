float d;

void setup(){
  Serial.begin(9600); 
  d = millis();
}

void loop(){
  if(millis()-d > 10000){
    d = millis();
    Serial.println(random(7)); 
  }
}
