package com.rainbow.chvz;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

public class FirstActivity extends AppCompatActivity {

    public static final String EXTRA_MSG = "MESSAGE";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_first);
    }

    /** Method to be called when user taps Send button */
    public void sendMessage( View view ) {
        // Do something when button is tapped
        Intent i = new Intent( this, SecondActivity_DisplayMsg.class );
        EditText e = (EditText) findViewById( R.id.editText );
        String m = e.getText().toString();
        i.putExtra( this.getPackageName() + EXTRA_MSG, m );
        startActivity( i );
    }
}
