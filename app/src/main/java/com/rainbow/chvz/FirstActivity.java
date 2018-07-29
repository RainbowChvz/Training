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
    public static final String BOOLEAN_DIALOG = "EXIT_DIALOG_SHOWING";

    private static AlertDialog exitDialog = null;
    private static boolean b_isExitDialogShowing = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        Log.i("LifeCycle", "onCreate: Bundle = " + savedInstanceState);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_first);
    }

    @Override
    protected void onRestart() {
        Log.i("LifeCycle", "onRestart" );
        super.onRestart();
    }

    @Override
    protected void onStart() {
        Log.i("LifeCycle", "onStart" );
        super.onStart();
    }

    @Override
    public void onRestoreInstanceState( Bundle savedInstanceState ) {
        Log.i("LifeCycle", "onRestoreInstanceState: Bundle = " + savedInstanceState );
        super.onRestoreInstanceState( savedInstanceState );

        if ( savedInstanceState.getBoolean( BOOLEAN_DIALOG, false ) )
            createExitDialog();
    }

    @Override
    protected void onResume() {
        Log.i("LifeCycle", "onResume" );
        super.onResume();

        if ( isExitDialogCreated() )
            showExitDialog();
    }

    @Override
    protected void onPause() {
        Log.i("LifeCycle", "onPause" );
        super.onPause();
    }

    @Override
    protected void onStop() {
        Log.i("LifeCycle", "onStop" );
        super.onStop();
    }

    @Override
    protected void onSaveInstanceState ( Bundle outState) {
        outState.putBoolean( BOOLEAN_DIALOG, isExitDialogShowing() );

        super.onSaveInstanceState( outState );
        Log.i("LifeCycle", "onSaveInstanceState: Bundle = " + outState );
    }

    @Override
    protected void onDestroy() {
        Log.i("LifeCycle", "onDestroy" );
        super.onDestroy();

        clearExitDialogShowing();
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
        Log.i("LifeCycle", "onBackPressed" );
        if ( !isExitDialogShowing() ) {
            if (!isExitDialogCreated())
                createExitDialog();
            showExitDialog();
        }
    }

    public boolean isExitDialogCreated () {
        return exitDialog != null;
    }

    public boolean isExitDialogShowing () {
        return b_isExitDialogShowing;
    }

    private void clearExitDialogShowing () {
        setExitDialogShowing( false );
        exitDialog = null;
    }

    public void setExitDialogShowing ( boolean showing ) {
        b_isExitDialogShowing = showing;
    }

    private void createExitDialog() {
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
        exitDialog = b.create();
        exitDialog.setOnDismissListener(new DialogInterface.OnDismissListener() {
                                        @Override
                                        public void onDismiss(DialogInterface dismiss) {
                                            clearExitDialogShowing();
                                        }
                                    }
        );
    }

    private void showExitDialog () {
        exitDialog.show();
        setExitDialogShowing( true );
    }
}
