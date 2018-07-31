package com.rainbow.chvz;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

public class SecondActivity_DisplayMsg extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second_display_msg);

        // Retrieve intent from caller activity
        Intent i = getIntent();
        // Retrieve string from Extras
        String m = i.getStringExtra( this.getPackageName() + FirstActivity.EXTRA_MSG );

        // Find TextView ID
        TextView t = findViewById( R.id.textView );
        // Set text for TextView ID
        t.setText( m );
    }
}
