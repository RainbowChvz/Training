package com.rainbow.chvz;

import android.content.DialogInterface;
import android.content.Intent;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class FirstActivity extends AppCompatActivity {

    public static final String EXTRA_MSG = "MESSAGE";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Log.i("LifeCycle", "onCreate: Bundle = " + savedInstanceState);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_first);
    }

    /** Method to be called when user taps Send button */
    public void onSend( View view ) {
        // Do something when button is tapped
        Intent i = new Intent( this, SecondActivity_DisplayMsg.class );
        EditText e = (EditText) findViewById( R.id.editText );
        String m = e.getText().toString();

        // If Send button is tapped without entering a message, display a toast notification
        if ( TextUtils.isEmpty( m ) ) {
            Toast.makeText(this, R.string.toast_empty_text, Toast.LENGTH_SHORT).show();
            return;
        }

        // If a message has been entered, go to next activity
        i.putExtra( this.getPackageName() + EXTRA_MSG, m );
        startActivity( i );
    }

    /** Method to be called when user presses Back button */
    @Override
    public void onBackPressed() {
        AlertDialog.Builder b = new AlertDialog.Builder(this);
        b.setTitle( R.string.dialog_exit_title );
        b.setMessage( R.string.dialog_exit_question );
        b.setPositiveButton(
                R.string.dialog_yes,
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        finish();
                    }
                }
        );
        b.setNegativeButton(
                R.string.dialog_no,
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        // 'No' option has been tapped
                    }
                }
        );

        AlertDialog d = b.create();
        d.show();
    }
}
