package com.rainbow.fragments;

import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {
    private static final String TAG = "MainActivity";

    private SectionsStatePagerAdapter mSectionsStatePagerAdapter;
    private ViewPager mViewPager;
    private Toast t;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Log.d(TAG, "onCreate: Started");

        mSectionsStatePagerAdapter = new SectionsStatePagerAdapter(getSupportFragmentManager());
        mViewPager = (ViewPager)findViewById(R.id.container);

        //setup the pager
        setupViewPager(mViewPager);
    }

    @Override
    public void onBackPressed() {
        if ( goBackViewPager() < 0)
            super.onBackPressed();
    }

    private void setupViewPager (ViewPager viewPager) {
        SectionsStatePagerAdapter adapter = new SectionsStatePagerAdapter(getSupportFragmentManager());
        adapter.addFragment( new Fragment1(), "Fragment 1");
        adapter.addFragment( new Fragment2(), "Fragment 2");
        adapter.addFragment( new Fragment3(), "Fragment 3");
        viewPager.setAdapter(adapter);
    }

    public void setViewPager ( int fragmentNumber) {
        mViewPager.setCurrentItem( fragmentNumber );
    }

    public int getViewPager () {
        return mViewPager.getCurrentItem();
    }

    private int goBackViewPager() {
        int item = getViewPager();
        if ( item-- > 0 )
            mViewPager.setCurrentItem( item );
        return item;
    }

    public void showToast( String text ) {
        if ( t == null )
            t = Toast.makeText(this, "Default msg", Toast.LENGTH_SHORT);
        t.setText( text );
        t.show();
    }
}
